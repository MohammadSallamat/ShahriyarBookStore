using Query.Common.Domain;

namespace Query.Products.DTOs;

public class ProductDto : BaseDto
{
    public string Title { get; set; } = string.Empty;
    public string ImageName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public long CategoryId { get; set; }
    public long SubCategoryId { get; set; }
    public long SecondarySubCategoryId { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string MetaTitle { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;
    public string MetaKeyWords { get; set; } = string.Empty;
    public bool IndexPage { get; set; }
    public string Canonical { get; set; } = string.Empty;
    public List<ProductImageDto> Images { get; set; } = new();
    public List<ProductSpecificationDto> Specifications { get; set; } = new();
}

public class ProductImageDto : BaseDto
{
    public long ProductId { get; set; }
    public string ImageName { get; set; } = string.Empty;
    public int Sequence { get; set; }
}

public class ProductSpecificationDto : BaseDto
{
    public long ProductId { get; set; }
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}

public class ProductFilterParams
{
    public int PageId { get; set; } = 1;
    public int Take { get; set; } = 10;
    public string? Title { get; set; }
    public long? CategoryId { get; set; }
    public string? Slug { get; set; }
}

public class ProductFilterResultDto
{
    public List<ProductDto> Products { get; set; } = new();
    public int PageId { get; set; }
    public int PageCount { get; set; }
    public int TotalCount { get; set; }
}
