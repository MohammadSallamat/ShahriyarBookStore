using Infrastructure.Persistent.EF;
using Microsoft.EntityFrameworkCore;
using Query.Admin.DTOs;
using Query.Common.Domain;

namespace Query.Admin.GetSellerInspection;

public class GetSellerInspectionQueryHandler : IBaseQueryHandler<GetSellerInspectionQuery, SellerInspectionDto?>
{
    private readonly ShahriarBookStoreContext _context;

    public GetSellerInspectionQueryHandler(ShahriarBookStoreContext context)
    {
        _context = context;
    }

    public async Task<SellerInspectionDto?> Handle(GetSellerInspectionQuery request, CancellationToken cancellationToken)
    {
        var seller = await _context.Sellers
            .Include(s => s.Inventories)
            .FirstOrDefaultAsync(s => s.Id == request.SellerId, cancellationToken);

        if (seller == null)
            return null;

        var productIds = seller.Inventories.Select(i => i.ProductId).ToList();
        var products = await _context.Products
            .Where(p => productIds.Contains(p.Id))
            .ToDictionaryAsync(p => p.Id, cancellationToken);

        var inventoryIds = seller.Inventories.Select(i => i.Id).ToList();

        var orders = await _context.Orders
            .Include(o => o.Items)
            .Where(o => o.Items.Any(item => inventoryIds.Contains(item.InventoryId)))
            .ToListAsync(cancellationToken);

        var soldOrders = new List<SellerOrderItemDto>();
        foreach (var order in orders)
        {
            foreach (var item in order.Items.Where(i => inventoryIds.Contains(i.InventoryId)))
            {
                var inventory = seller.Inventories.FirstOrDefault(i => i.Id == item.InventoryId);
                var productTitle = inventory != null && products.ContainsKey(inventory.ProductId)
                    ? products[inventory.ProductId].Title
                    : "محصول نامشخص";

                soldOrders.Add(new SellerOrderItemDto
                {
                    OrderId = order.Id,
                    OrderCreationDate = order.CreationDate,
                    OrderStatus = order.Status,
                    ProductId = inventory?.ProductId ?? 0,
                    ProductTitle = productTitle,
                    Count = item.Count,
                    Price = item.Price
                });
            }
        }

        var inventoryDtos = seller.Inventories.Select(i => new SellerInventoryDetailDto
        {
            Id = i.Id,
            CreationDate = i.CreationDate,
            ProductId = i.ProductId,
            ProductTitle = products.ContainsKey(i.ProductId) ? products[i.ProductId].Title : string.Empty,
            ProductImage = products.ContainsKey(i.ProductId) ? products[i.ProductId].ImageName : string.Empty,
            Count = i.Count,
            Price = i.Price,
            DiscountPercentage = i.DiscountPercentage
        }).ToList();

        return new SellerInspectionDto
        {
            Id = seller.Id,
            CreationDate = seller.CreationDate,
            UserId = seller.UserId,
            ShopName = seller.ShopName,
            NationalCode = seller.NationalCode,
            Status = seller.Status,
            LastUpdate = seller.LastUpdate,
            TotalInventories = seller.Inventories.Count,
            Inventories = inventoryDtos,
            SoldOrders = soldOrders
        };
    }
}
