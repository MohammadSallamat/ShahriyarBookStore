using Infrastructure.Persistent.EF;
using Microsoft.EntityFrameworkCore;
using Query.Categories.DTOs;
using Query.Common.Domain;

namespace Query.Categories.GetById;

public class GetCategoryByIdQueryHandler : IBaseQueryHandler<GetCategoryByIdQuery, CategoryDto?>
{
    private readonly ShahriarBookStoreContext _context;

    public GetCategoryByIdQueryHandler(ShahriarBookStoreContext context)
    {
        _context = context;
    }

    public async Task<CategoryDto?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .Include(c => c.Childs)
            .FirstOrDefaultAsync(c => c.Id == request.CategoryId, cancellationToken);

        if (category == null)
            return null;

        return MapCategory(category);
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
