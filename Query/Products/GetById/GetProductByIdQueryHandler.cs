using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistent.EF;
using Query.Common.Domain;
using Query.Products.DTOs;

namespace Query.Products.GetById;

public class GetProductByIdQueryHandler : IBaseQueryHandler<GetProductByIdQuery, ProductDto?>
{
    private readonly ShahriarBookStoreContext _context;

    public GetProductByIdQueryHandler(ShahriarBookStoreContext context)
    {
        _context = context;
    }

    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .AsNoTracking()
            .Include(p => p.Images)
            .Include(p => p.Specifications)
            .FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);

        if (product == null)
            return null;

        return MapToProductDto(product);
    }

    public static ProductDto MapToProductDto(Domain.ProductAggregate.Product product)
    {
        return new ProductDto
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
            MetaTitle = product.SeoData?.MetaTitle ?? string.Empty,
            MetaDescription = product.SeoData?.MetaDescription ?? string.Empty,
            MetaKeyWords = product.SeoData?.MetaKeyWords ?? string.Empty,
            IndexPage = product.SeoData?.IndexPage ?? false,
            Canonical = product.SeoData?.Canonical ?? string.Empty,
            Images = product.Images?.Select(i => new ProductImageDto
            {
                Id = i.Id,
                CreationDate = i.CreationDate,
                ProductId = i.ProductId,
                ImageName = i.ImageName,
                Sequence = i.Sequence
            }).ToList() ?? new List<ProductImageDto>(),
            Specifications = product.Specifications?.Select(s => new ProductSpecificationDto
            {
                Id = s.Id,
                CreationDate = s.CreationDate,
                ProductId = s.ProductId,
                Key = s.Key,
                Value = s.Value
            }).ToList() ?? new List<ProductSpecificationDto>()
        };
    }
}
