using Infrastructure.Persistent.EF;
using Microsoft.EntityFrameworkCore;
using Query.Admin.DTOs;
using Query.Common.Domain;

namespace Query.Admin.GetCategoryStats;

public class GetCategoryStatsQueryHandler : IBaseQueryHandler<GetCategoryStatsQuery, List<CategoryBookCountDto>>
{
    private readonly ShahriarBookStoreContext _context;

    public GetCategoryStatsQueryHandler(ShahriarBookStoreContext context)
    {
        _context = context;
    }

    public async Task<List<CategoryBookCountDto>> Handle(GetCategoryStatsQuery request, CancellationToken cancellationToken)
    {
        var categories = await _context.Categories.ToListAsync(cancellationToken);
        var products = await _context.Products.Select(p => p.CategoryId).ToListAsync(cancellationToken);

        return categories.Select(c => new CategoryBookCountDto
        {
            CategoryId = c.Id,
            CategoryTitle = c.Title,
            Slug = c.Slug,
            BookCount = products.Count(p => p == c.Id)
        }).OrderByDescending(c => c.BookCount).ToList();
    }
}
