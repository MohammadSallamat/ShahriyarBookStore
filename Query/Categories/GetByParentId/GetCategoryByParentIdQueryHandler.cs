using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistent.EF;
using Query.Categories.DTOs;
using Query.Categories.GetById;
using Query.Categories.GetByParentId;
using Query.Common.Domain;

namespace Query.Categories.GetByParentId;

public class GetCategoryByParentIdQueryHandler : IBaseQueryHandler<GetCategoryByParentIdQuery, List<CategoryDto>>
{
    private readonly ShahriarBookStoreContext _context;

    public GetCategoryByParentIdQueryHandler(ShahriarBookStoreContext context)
    {
        _context = context;
    }

    public async Task<List<CategoryDto>> Handle(GetCategoryByParentIdQuery request, CancellationToken cancellationToken)
    {
        var categories = await _context.Categories
            .AsNoTracking()
            .Where(c => c.ParentId == request.ParentId)
            .Include(c => c.Childs)
            .ToListAsync(cancellationToken);

        return categories.Select(GetCategoryByIdQueryHandler.MapToCategoryDto).ToList();
    }
}
