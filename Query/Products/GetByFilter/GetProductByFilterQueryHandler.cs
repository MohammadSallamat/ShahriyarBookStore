using Infrastructure.Persistent.EF;
using Microsoft.EntityFrameworkCore;
using Query.Common.Domain;
using Query.Products.DTOs;
using Query.Products.GetById;

namespace Query.Products.GetByFilter;

public class GetProductByFilterQueryHandler : IBaseQueryHandler<GetProductByFilterQuery, ProductFilterResult>
{
    private readonly ShahriarBookStoreContext _context;

    public GetProductByFilterQueryHandler(ShahriarBookStoreContext context)
    {
        _context = context;
    }

    public async Task<ProductFilterResult> Handle(GetProductByFilterQuery request, CancellationToken cancellationToken)
    {
        var filter = request.FilterParams;
        var query = _context.Products
            .Include(p => p.Images)
            .Include(p => p.Specifications)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            query = query.Where(p => p.Title.Contains(filter.Search) || p.Description.Contains(filter.Search));
        }

        if (filter.CategoryId.HasValue && filter.CategoryId.Value > 0)
        {
            query = query.Where(p => p.CategoryId == filter.CategoryId.Value ||
                                     p.SubCategoryId == filter.CategoryId.Value ||
                                     p.SecondarySubCategoryId == filter.CategoryId.Value);
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var skip = (filter.PageId - 1) * filter.Take;
        var products = await query
            .OrderByDescending(p => p.Id)
            .Skip(skip)
            .Take(filter.Take)
            .ToListAsync(cancellationToken);

        return new ProductFilterResult
        {
            TotalCount = totalCount,
            PageId = filter.PageId,
            Take = filter.Take,
            Products = products.Select(GetProductByIdQueryHandler.MapProduct).ToList()
        };
    }
}
