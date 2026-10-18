using Query.Common.Domain;
using Query.Products.DTOs;

namespace Query.Products.GetByFilter;

public record GetProductByFilterQuery(ProductFilterParams FilterParams) : IBaseQuery<ProductFilterResult>;
