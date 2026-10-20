using Query.Admin.DTOs;
using Query.Common.Domain;

namespace Query.Admin.GetCategoryStats;

public record GetCategoryStatsQuery : IBaseQuery<List<CategoryBookCountDto>>;
