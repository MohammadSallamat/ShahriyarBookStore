using Infrastructure.Persistent.EF;
using Microsoft.EntityFrameworkCore;
using Query.Common.Domain;
using Query.Products.DTOs;
using Query.Products.GetById;

namespace Query.Products.GetBySlug;

public class GetProductBySlugQueryHandler : IBaseQueryHandler<GetProductBySlugQuery, ProductDto?>
{
    private readonly ShahriarBookStoreContext _context;

    public GetProductBySlugQueryHandler(ShahriarBookStoreContext context)
    {
        _context = context;
    }

    public async Task<ProductDto?> Handle(GetProductBySlugQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .Include(p => p.Images)
            .Include(p => p.Specifications)
            .FirstOrDefaultAsync(p => p.Slug == request.Slug, cancellationToken);

        if (product == null)
            return null;

        return GetProductByIdQueryHandler.MapProduct(product);
    }
}
