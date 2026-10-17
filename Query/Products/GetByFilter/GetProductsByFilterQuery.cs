using Query.Common.Domain;
using Query.Products.DTOs;

namespace Query.Products.GetByFilter;

public record GetProductsByFilterQuery(ProductFilterParams FilterParams) : IBaseQuery<ProductFilterResultDto>;
