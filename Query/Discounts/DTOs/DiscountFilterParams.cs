using System;
using System.Collections.Generic;

namespace Query.Discounts.DTOs;

public class DiscountFilterParams
{
    public string? Code { get; set; }
    public bool? IsActive { get; set; }
    public int PageId { get; set; } = 1;
    public int Take { get; set; } = 10;
}

public class DiscountFilterResult
{
    public List<DiscountDto> Discounts { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageId { get; set; }
    public int Take { get; set; }
    public int PageCount => (int)Math.Ceiling((double)TotalCount / (Take == 0 ? 1 : Take));
}
