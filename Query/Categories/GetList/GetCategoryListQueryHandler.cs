using Infrastructure.Persistent.EF;
using Microsoft.EntityFrameworkCore;
using Query.Categories.DTOs;
using Query.Common.Domain;

namespace Query.Categories.GetList;

internal class GetCategoryListQueryHandler : IBaseQueryHandler<GetCategoryListQuery, List<CategoryDto>>
{
    private readonly ShahriarBookStoreContext _context;

    public GetCategoryListQueryHandler(ShahriarBookStoreContext context)
    {
        _context = context;
    }

    public async Task<List<CategoryDto>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
    {
        var categories = await _context.Categories
            .Where(c => c.ParentId == null)
            .Include(c => c.Childs)
            .ToListAsync(cancellationToken);

        return categories.Select(category => new CategoryDto
        {
            Id = category.Id,
            CreationDate = category.CreationDate,
            Title = category.Title,
            Slug = category.Slug,
            SeoData = category.SeoData,
            ParentId = category.ParentId,
            Childs = category.Childs?.Select(child => new ChildCategoryDto
            {
                Id = child.Id,
                CreationDate = child.CreationDate,
                Title = child.Title,
                Slug = child.Slug,
                SeoData = child.SeoData,
                ParentId = child.ParentId ?? category.Id
            }).ToList() ?? new List<ChildCategoryDto>()
        }).ToList();
    }
}
