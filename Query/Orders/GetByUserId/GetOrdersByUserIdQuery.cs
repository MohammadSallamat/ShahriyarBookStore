using Query.Common.Domain;
using Query.Orders.DTOs;

namespace Query.Orders.GetByUserId;

public record GetOrdersByUserIdQuery(long UserId) : IBaseQuery<List<OrderDto>>;
