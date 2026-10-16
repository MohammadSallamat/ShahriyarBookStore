using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistent.EF;
using Query.Categories.DTOs;
using Query.Categories.GetById;
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
            .AsNoTracking()
            .Include(c => c.Childs)
            .FirstOrDefaultAsync(c => c.Id == request.CategoryId, cancellationToken);

        if (category == null)
            return null;

        return MapToCategoryDto(category);
    }

    public static CategoryDto MapToCategoryDto(Domain.CategoryAggregate.Category category)
    {
        return new CategoryDto
        {
            Id = category.Id,
            CreationDate = category.CreationDate,
            Title = category.Title,
            Slug = category.Slug,
            ParentId = category.ParentId,
            MetaTitle = category.SeoData?.MetaTitle ?? string.Empty,
            MetaDescription = category.SeoData?.MetaDescription ?? string.Empty,
            MetaKeyWords = category.SeoData?.MetaKeyWords ?? string.Empty,
            IndexPage = category.SeoData?.IndexPage ?? false,
            Canonical = category.SeoData?.Canonical ?? string.Empty,
            ChildCategories = category.Childs?.Select(MapToCategoryDto).ToList() ?? new List<CategoryDto>()
        };
    }
}
