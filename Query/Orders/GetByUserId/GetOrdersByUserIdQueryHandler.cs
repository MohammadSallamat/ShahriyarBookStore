using Infrastructure.Persistent.EF;
using Microsoft.EntityFrameworkCore;
using Query.Common.Domain;
using Query.Orders.DTOs;
using Query.Orders.GetById;

namespace Query.Orders.GetByUserId;

public class GetOrdersByUserIdQueryHandler : IBaseQueryHandler<GetOrdersByUserIdQuery, List<OrderDto>>
{
    private readonly ShahriarBookStoreContext _context;

    public GetOrdersByUserIdQueryHandler(ShahriarBookStoreContext context)
    {
        _context = context;
    }

    public async Task<List<OrderDto>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
    {
        var orders = await _context.Orders
            .Include(o => o.Items)
            .Where(o => o.UserId == request.UserId)
            .OrderByDescending(o => o.Id)
            .ToListAsync(cancellationToken);

        return orders.Select(GetOrderByIdQueryHandler.MapOrder).ToList();
    }
}
