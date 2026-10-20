namespace Query.Products.DTOs;

public class ProductFilterParams
{
    public string? Search { get; set; }
    public long? CategoryId { get; set; }
    public int PageId { get; set; } = 1;
    public int Take { get; set; } = 10;
}

public class ProductFilterResult
{
    public List<ProductDto> Products { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageId { get; set; }
    public int Take { get; set; }
    public int PageCount => (int)Math.Ceiling((double)TotalCount / (Take == 0 ? 1 : Take));
}
