using Infrastructure.Persistent.EF;
using Microsoft.EntityFrameworkCore;
using Query.Common.Domain;
using Query.Orders.DTOs;

namespace Query.Orders.GetById;

public class GetOrderByIdQueryHandler : IBaseQueryHandler<GetOrderByIdQuery, OrderDto?>
{
    private readonly ShahriarBookStoreContext _context;

    public GetOrderByIdQueryHandler(ShahriarBookStoreContext context)
    {
        _context = context;
    }

    public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);

        if (order == null)
            return null;

        return MapOrder(order);
    }

    public static OrderDto MapOrder(Domain.OrderAggregate.Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            CreationDate = order.CreationDate,
            UserId = order.UserId,
            Status = order.Status,
            Address = order.Address,
            ShippingMethod = order.ShippingMethod,
            Items = order.Items?.Select(i => new OrderItemDto
            {
                Id = i.Id,
                CreationDate = i.CreationDate,
                OrderId = i.OrderId,
                InventoryId = i.InventoryId,
                Count = i.Count,
                Price = i.Price
            }).ToList() ?? new()
        };
    }
}
