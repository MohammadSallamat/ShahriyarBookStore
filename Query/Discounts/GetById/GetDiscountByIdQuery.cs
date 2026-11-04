using Query.Common.Domain;
using Query.Discounts.DTOs;

namespace Query.Discounts.GetById;

public record GetDiscountByIdQuery(long DiscountId) : IBaseQuery<DiscountDto?>;
