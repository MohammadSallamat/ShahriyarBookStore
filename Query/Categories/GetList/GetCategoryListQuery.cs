using Query.Categories.DTOs;
using Query.Common.Domain;
using System.Collections.Generic;

namespace Query.Categories.GetList;

public record GetCategoryListQuery : IBaseQuery<List<CategoryDto>>;
