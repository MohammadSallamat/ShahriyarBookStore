using Query.Categories.DTOs;
using Query.Common.Domain;

namespace Query.Categories.GetList;

public record GetCategoriesListQuery : IBaseQuery<List<CategoryDto>>;
