using Query.Categories.DTOs;
using Query.Common.Domain;
using System.Collections.Generic;

namespace Query.Categories.GetChildsByParentId;

public record GetCategoryChildsByParentIdQuery(long ParentId) : IBaseQuery<List<ChildCategoryDto>>;
