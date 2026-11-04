using Infrastructure.Persistent.EF;
using Microsoft.EntityFrameworkCore;
using Query.Common.Domain;
using Query.Products.DTOs;
using Query.Wishlists.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Query.Wishlists.GetByUserId;

public class GetUserWishlistQueryHandler : IBaseQueryHandler<GetUserWishlistQuery, List<WishlistDto>>
{
    private readonly ShahriarBookStoreContext _context;

    public GetUserWishlistQueryHandler(ShahriarBookStoreContext context)
    {
        _context = context;
    }

    public async Task<List<WishlistDto>> Handle(GetUserWishlistQuery request, CancellationToken cancellationToken)
    {
        var wishlists = await _context.Wishlists
            .Where(w => w.UserId == request.UserId)
            .OrderByDescending(w => w.Id)
            .ToListAsync(cancellationToken);

        if (!wishlists.Any())
            return new List<WishlistDto>();

        var productIds = wishlists.Select(w => w.ProductId).Distinct().ToList();
        var products = await _context.Products
            .Where(p => productIds.Contains(p.Id))
            .ToDictionaryAsync(p => p.Id, cancellationToken);

        return wishlists.Select(w => new WishlistDto
        {
            Id = w.Id,
            CreationDate = w.CreationDate,
            UserId = w.UserId,
            ProductId = w.ProductId,
            Product = products.TryGetValue(w.ProductId, out var product)
                ? new ProductDto
                {
                    Id = product.Id,
                    CreationDate = product.CreationDate,
                    Title = product.Title,
                    ImageName = product.ImageName,
                    Description = product.Description,
                    CategoryId = product.CategoryId,
                    SubCategoryId = product.SubCategoryId,
                    SecondarySubCategoryId = product.SecondarySubCategoryId,
                    Slug = product.Slug,
                    SeoData = product.SeoData
                }
                : null
        }).ToList();
    }
}
