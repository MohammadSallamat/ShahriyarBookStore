using Domain.Common.Domain.ValueObjects;
using Query.Common.Domain;
using System.Collections.Generic;

namespace Query.Categories.DTOs;

public class CategoryDto : BaseDto
{
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public SeoData? SeoData { get; set; }
    public long? ParentId { get; set; }
    public List<ChildCategoryDto> Childs { get; set; } = new();
}

public class ChildCategoryDto : BaseDto
{
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public SeoData? SeoData { get; set; }
    public long ParentId { get; set; }
}
