using Domain.OrderAggregate;
using Domain.OrderAggregate.Enums;
using Domain.OrderAggregate.ValueObjects;
using Query.Common.Domain;

namespace Query.Orders.DTOs;

public class OrderDto : BaseDto
{
    public long UserId { get; set; }
    public OrderStatus Status { get; set; }
    public OrderAddress? Address { get; set; }
    public ShippingMethod? ShippingMethod { get; set; }
    public List<OrderItemDto> Items { get; set; } = new();
    public int TotalPrice => Items.Sum(i => i.TotalPrice);
}

public class OrderItemDto : BaseDto
{
    public long OrderId { get; set; }
    public long InventoryId { get; set; }
    public int Count { get; set; }
    public int Price { get; set; }
    public int TotalPrice => Count * Price;
}
