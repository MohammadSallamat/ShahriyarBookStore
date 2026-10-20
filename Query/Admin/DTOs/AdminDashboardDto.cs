using Domain.OrderAggregate.Enums;
using Domain.SellerAggregate.Enums;
using Query.Common.Domain;

namespace Query.Admin.DTOs;

public class AdminDashboardSummaryDto
{
    public int TotalUsers { get; set; }
    public int TotalSellers { get; set; }
    public int PendingSellersCount { get; set; }
    public int AcceptedSellersCount { get; set; }
    public int RejectedSellersCount { get; set; }
    public int TotalProducts { get; set; }
    public int TotalOrders { get; set; }
    public long TotalRevenue { get; set; }
    public List<CategoryBookCountDto> CategoryStats { get; set; } = new();
}

public class CategoryBookCountDto
{
    public long CategoryId { get; set; }
    public string CategoryTitle { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public int BookCount { get; set; }
}

public class SellerInspectionDto : BaseDto
{
    public long UserId { get; set; }
    public string ShopName { get; set; } = string.Empty;
    public string NationalCode { get; set; } = string.Empty;
    public SellerStatus Status { get; set; }
    public DateTime? LastUpdate { get; set; }
    public int TotalInventories { get; set; }
    public List<SellerInventoryDetailDto> Inventories { get; set; } = new();
    public List<SellerOrderItemDto> SoldOrders { get; set; } = new();
}

public class SellerInventoryDetailDto : BaseDto
{
    public long ProductId { get; set; }
    public string ProductTitle { get; set; } = string.Empty;
    public string ProductImage { get; set; } = string.Empty;
    public int Count { get; set; }
    public int Price { get; set; }
    public int? DiscountPercentage { get; set; }
}

public class SellerOrderItemDto
{
    public long OrderId { get; set; }
    public DateTime OrderCreationDate { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public long ProductId { get; set; }
    public string ProductTitle { get; set; } = string.Empty;
    public int Count { get; set; }
    public int Price { get; set; }
    public int TotalPrice => Price * Count;
}
