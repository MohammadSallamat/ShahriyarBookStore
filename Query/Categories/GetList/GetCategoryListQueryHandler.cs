using Infrastructure.Persistent.EF;
using Microsoft.EntityFrameworkCore;
using Query.Categories.DTOs;
using Query.Common.Domain;

namespace Query.Categories.GetList;

public class GetCategoryListQueryHandler : IBaseQueryHandler<GetCategoryListQuery, List<CategoryDto>>
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

        return categories.Select(MapCategory).ToList();
    }

    private static CategoryDto MapCategory(Domain.CategoryAggregate.Category category)
    {
        return new CategoryDto
        {
            Id = category.Id,
            CreationDate = category.CreationDate,
            Title = category.Title,
            Slug = category.Slug,
            SeoData = category.SeoData,
            ParentId = category.ParentId,
            Childs = category.Childs?.Select(MapCategory).ToList() ?? new List<CategoryDto>()
        };
    }
}
