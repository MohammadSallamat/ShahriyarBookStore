using Query.Common.Domain;
using Query.SiteEntities.DTOs;

namespace Query.SiteEntities.GetSliders;

public record GetSlidersQuery : IBaseQuery<List<SliderDto>>;
