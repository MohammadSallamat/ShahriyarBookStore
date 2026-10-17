using Query.Categories.DTOs;
using Query.Common.Domain;

namespace Query.Categories.GetBySlug;

public record GetCategoryBySlugQuery(string Slug) : IBaseQuery<CategoryDto>;
