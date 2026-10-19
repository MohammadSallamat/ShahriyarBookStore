using Query.Common.Domain;
using Query.Products.DTOs;

namespace Query.Products.GetById;

public record GetProductByIdQuery(long ProductId) : IBaseQuery<ProductDto?>;
