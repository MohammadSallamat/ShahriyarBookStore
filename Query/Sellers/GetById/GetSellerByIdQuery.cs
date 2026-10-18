using Query.Common.Domain;
using Query.Sellers.DTOs;

namespace Query.Sellers.GetById;

public record GetSellerByIdQuery(long SellerId) : IBaseQuery<SellerDto?>;
