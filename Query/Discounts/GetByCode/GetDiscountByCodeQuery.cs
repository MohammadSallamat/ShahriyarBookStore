using Query.Common.Domain;
using Query.Discounts.DTOs;

namespace Query.Discounts.GetByCode;

public record GetDiscountByCodeQuery(string Code) : IBaseQuery<DiscountDto?>;
