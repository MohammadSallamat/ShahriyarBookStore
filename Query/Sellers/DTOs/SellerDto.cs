using Domain.SellerAggregate.Enums;
using Query.Common.Domain;

namespace Query.Sellers.DTOs;

public class SellerDto : BaseDto
{
    public long UserId { get; set; }
    public string ShopName { get; set; } = string.Empty;
    public string NationalCode { get; set; } = string.Empty;
    public SellerStatus Status { get; set; }
    public List<SellerInventoryDto> Inventories { get; set; } = new();
}

public class SellerInventoryDto : BaseDto
{
    public long SellerId { get; set; }
    public long ProductId { get; set; }
    public int Count { get; set; }
    public int Price { get; set; }
    public int? DiscountPercentage { get; set; }
}
