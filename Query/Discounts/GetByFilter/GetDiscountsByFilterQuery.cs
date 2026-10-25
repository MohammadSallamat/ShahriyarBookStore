using Query.Common.Domain;
using Query.Discounts.DTOs;

namespace Query.Discounts.GetByFilter;

public record GetDiscountsByFilterQuery(DiscountFilterParams FilterParams) : IBaseQuery<DiscountFilterResult>;
