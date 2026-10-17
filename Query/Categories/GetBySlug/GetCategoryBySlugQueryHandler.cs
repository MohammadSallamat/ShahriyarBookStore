using Infrastructure.Persistent.EF;
using Microsoft.EntityFrameworkCore;
using Query.Categories.DTOs;
using Query.Common.Domain;

namespace Query.Categories.GetBySlug;

internal class GetCategoryBySlugQueryHandler : IBaseQueryHandler<GetCategoryBySlugQuery, CategoryDto>
{
    private readonly ShahriarBookStoreContext _context;

    public GetCategoryBySlugQueryHandler(ShahriarBookStoreContext context)
    {
        _context = context;
    }

    public async Task<CategoryDto> Handle(GetCategoryBySlugQuery request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .Include(c => c.Childs)
            .FirstOrDefaultAsync(c => c.Slug == request.Slug, cancellationToken);

        if (category == null)
            return null;

        return new CategoryDto
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
        };
    }
}
