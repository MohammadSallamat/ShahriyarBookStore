using Infrastructure.Persistent.EF;
using Microsoft.EntityFrameworkCore;
using Query.Categories.DTOs;
using Query.Common.Domain;

namespace Query.Categories.GetChildsByParentId;

internal class GetCategoryChildsByParentIdQueryHandler : IBaseQueryHandler<GetCategoryChildsByParentIdQuery, List<ChildCategoryDto>>
{
    private readonly ShahriarBookStoreContext _context;

    public GetCategoryChildsByParentIdQueryHandler(ShahriarBookStoreContext context)
    {
        _context = context;
    }

    public async Task<List<ChildCategoryDto>> Handle(GetCategoryChildsByParentIdQuery request, CancellationToken cancellationToken)
    {
        var childs = await _context.Categories
            .Where(c => c.ParentId == request.ParentId)
            .ToListAsync(cancellationToken);

        return childs.Select(child => new ChildCategoryDto
        {
            Id = child.Id,
            CreationDate = child.CreationDate,
            Title = child.Title,
            Slug = child.Slug,
            SeoData = child.SeoData,
            ParentId = child.ParentId ?? request.ParentId
        }).ToList();
    }
}
