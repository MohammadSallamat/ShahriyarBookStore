using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistent.EF;
using Query.Categories.DTOs;
using Query.Categories.GetById;
using Query.Categories.GetList;
using Query.Common.Domain;

namespace Query.Categories.GetList;

public class GetCategoriesListQueryHandler : IBaseQueryHandler<GetCategoriesListQuery, List<CategoryDto>>
{
    private readonly ShahriarBookStoreContext _context;

    public GetCategoriesListQueryHandler(ShahriarBookStoreContext context)
    {
        _context = context;
    }

    public async Task<List<CategoryDto>> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
    {
        var categories = await _context.Categories
            .AsNoTracking()
            .Where(c => c.ParentId == null)
            .Include(c => c.Childs)
            .ToListAsync(cancellationToken);

        return categories.Select(GetCategoryByIdQueryHandler.MapToCategoryDto).ToList();
    }
}
