using Infrastructure.Persistent.EF;
using Microsoft.EntityFrameworkCore;
using Query.Common.Domain;
using Query.Sellers.DTOs;

namespace Query.Sellers.GetById;

public class GetSellerByIdQueryHandler : IBaseQueryHandler<GetSellerByIdQuery, SellerDto?>
{
    private readonly ShahriarBookStoreContext _context;

    public GetSellerByIdQueryHandler(ShahriarBookStoreContext context)
    {
        _context = context;
    }

    public async Task<SellerDto?> Handle(GetSellerByIdQuery request, CancellationToken cancellationToken)
    {
        var seller = await _context.Sellers
            .Include(s => s.Inventories)
            .FirstOrDefaultAsync(s => s.Id == request.SellerId, cancellationToken);

        if (seller == null)
            return null;

        return MapSeller(seller);
    }

    public static SellerDto MapSeller(Domain.SellerAggregate.Seller seller)
    {
        return new SellerDto
        {
            Id = seller.Id,
            CreationDate = seller.CreationDate,
            UserId = seller.UserId,
            ShopName = seller.ShopName,
            NationalCode = seller.NationalCode,
            Status = seller.Status,
            Inventories = seller.Inventories?.Select(i => new SellerInventoryDto
            {
                Id = i.Id,
                CreationDate = i.CreationDate,
                SellerId = i.SellerId,
                ProductId = i.ProductId,
                Count = i.Count,
                Price = i.Price,
                DiscountPercentage = i.DiscountPercentage
            }).ToList() ?? new()
        };
    }
}
