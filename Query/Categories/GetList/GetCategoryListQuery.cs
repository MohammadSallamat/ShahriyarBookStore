using Query.Categories.DTOs;
using Query.Common.Domain;

namespace Query.Categories.GetList;

public record GetCategoryListQuery : IBaseQuery<List<CategoryDto>>;
