using Query.Categories.DTOs;
using Query.Common.Domain;

namespace Query.Categories.GetByParentId;

public record GetCategoryByParentIdQuery(long ParentId) : IBaseQuery<List<CategoryDto>>;
