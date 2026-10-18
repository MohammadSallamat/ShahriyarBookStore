using Query.Common.Domain;
using Query.Products.DTOs;

namespace Query.Products.GetBySlug;

public record GetProductBySlugQuery(string Slug) : IBaseQuery<ProductDto?>;
