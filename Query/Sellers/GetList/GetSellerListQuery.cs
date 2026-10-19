using Query.Common.Domain;
using Query.Sellers.DTOs;

namespace Query.Sellers.GetList;

public record GetSellerListQuery : IBaseQuery<List<SellerDto>>;
