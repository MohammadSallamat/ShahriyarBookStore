using Domain.DiscountAggregate.Enums;
using Query.Common.Domain;
using System;

namespace Query.Discounts.DTOs;

public class DiscountDto : BaseDto
{
    public string Code { get; set; }
    public DiscountType DiscountType { get; set; }
    public int DiscountAmount { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? Capacity { get; set; }
    public int UsedCount { get; set; }
    public int? MinOrderAmount { get; set; }
    public int? MaxDiscountAmount { get; set; }
    public bool IsActive { get; set; }
}
