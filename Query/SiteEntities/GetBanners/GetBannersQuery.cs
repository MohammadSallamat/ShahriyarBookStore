using Query.Common.Domain;
using Query.SiteEntities.DTOs;

namespace Query.SiteEntities.GetBanners;

public record GetBannersQuery : IBaseQuery<List<BannerDto>>;
