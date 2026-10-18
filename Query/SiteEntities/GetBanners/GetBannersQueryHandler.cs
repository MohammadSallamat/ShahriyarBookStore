using Infrastructure.Persistent.EF;
using Microsoft.EntityFrameworkCore;
using Query.Common.Domain;
using Query.SiteEntities.DTOs;

namespace Query.SiteEntities.GetBanners;

public class GetBannersQueryHandler : IBaseQueryHandler<GetBannersQuery, List<BannerDto>>
{
    private readonly ShahriarBookStoreContext _context;

    public GetBannersQueryHandler(ShahriarBookStoreContext context)
    {
        _context = context;
    }

    public async Task<List<BannerDto>> Handle(GetBannersQuery request, CancellationToken cancellationToken)
    {
        var banners = await _context.Banners.ToListAsync(cancellationToken);
        return banners.Select(b => new BannerDto
        {
            Id = b.Id,
            CreationDate = b.CreationDate,
            Link = b.Link,
            ImageName = b.ImageName,
            Position = b.Position
        }).ToList();
    }
}
