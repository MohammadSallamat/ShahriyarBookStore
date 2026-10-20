using Query.Common.Domain;
using Query.Orders.DTOs;

namespace Query.Orders.GetById;

public record GetOrderByIdQuery(long OrderId) : IBaseQuery<OrderDto?>;
