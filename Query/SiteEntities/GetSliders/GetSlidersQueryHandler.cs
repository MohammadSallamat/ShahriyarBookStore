using Infrastructure.Persistent.EF;
using Microsoft.EntityFrameworkCore;
using Query.Common.Domain;
using Query.SiteEntities.DTOs;

namespace Query.SiteEntities.GetSliders;

public class GetSlidersQueryHandler : IBaseQueryHandler<GetSlidersQuery, List<SliderDto>>
{
    private readonly ShahriarBookStoreContext _context;

    public GetSlidersQueryHandler(ShahriarBookStoreContext context)
    {
        _context = context;
    }

    public async Task<List<SliderDto>> Handle(GetSlidersQuery request, CancellationToken cancellationToken)
    {
        var sliders = await _context.Sliders.ToListAsync(cancellationToken);
        return sliders.Select(s => new SliderDto
        {
            Id = s.Id,
            CreationDate = s.CreationDate,
            Title = s.Title,
            Link = s.Link,
            ImageName = s.ImageName
        }).ToList();
    }
}
