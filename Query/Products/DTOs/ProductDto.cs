using Domain.Common.Domain.ValueObjects;
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
    public SeoData? SeoData { get; set; }
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
