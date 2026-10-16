using Query.Common.Domain;

namespace Query.Categories.DTOs;

public class CategoryDto : BaseDto
{
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string MetaTitle { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;
    public string MetaKeyWords { get; set; } = string.Empty;
    public bool IndexPage { get; set; }
    public string Canonical { get; set; } = string.Empty;
    public long? ParentId { get; set; }
    public List<CategoryDto> ChildCategories { get; set; } = new();
}
