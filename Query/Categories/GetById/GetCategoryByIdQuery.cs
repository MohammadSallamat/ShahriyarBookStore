using Query.Categories.DTOs;
using Query.Common.Domain;

namespace Query.Categories.GetById;

public record GetCategoryByIdQuery(long CategoryId) : IBaseQuery<CategoryDto?>;
