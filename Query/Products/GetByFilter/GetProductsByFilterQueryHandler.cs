using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistent.EF;
using Query.Common.Domain;
using Query.Products.DTOs;
using Query.Products.GetById;

namespace Query.Products.GetByFilter;

public class GetProductsByFilterQueryHandler : IBaseQueryHandler<GetProductsByFilterQuery, ProductFilterResultDto>
{
    private readonly ShahriarBookStoreContext _context;

    public GetProductsByFilterQueryHandler(ShahriarBookStoreContext context)
    {
        _context = context;
    }

    public async Task<ProductFilterResultDto> Handle(GetProductsByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterParams;
        var query = _context.Products
            .AsNoTracking()
            .Include(p => p.Images)
            .Include(p => p.Specifications)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(@params.Title))
            query = query.Where(p => p.Title.Contains(@params.Title));

        if (@params.CategoryId.HasValue)
            query = query.Where(p => p.CategoryId == @params.CategoryId.Value ||
                                     p.SubCategoryId == @params.CategoryId.Value ||
                                     p.SecondarySubCategoryId == @params.CategoryId.Value);

        if (!string.IsNullOrWhiteSpace(@params.Slug))
            query = query.Where(p => p.Slug == @params.Slug);

        var totalCount = await query.CountAsync(cancellationToken);
        var pageId = @params.PageId < 1 ? 1 : @params.PageId;
        var take = @params.Take < 1 ? 10 : @params.Take;
        var pageCount = (int)Math.Ceiling((double)totalCount / take);

        var products = await query
            .OrderByDescending(p => p.Id)
            .Skip((pageId - 1) * take)
            .Take(take)
            .ToListAsync(cancellationToken);

        return new ProductFilterResultDto
        {
            Products = products.Select(GetProductByIdQueryHandler.MapToProductDto).ToList(),
            PageId = pageId,
            PageCount = pageCount,
            TotalCount = totalCount
        };
    }
}
