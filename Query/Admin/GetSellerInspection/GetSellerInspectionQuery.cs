using Query.Admin.DTOs;
using Query.Common.Domain;

namespace Query.Admin.GetSellerInspection;

public record GetSellerInspectionQuery(long SellerId) : IBaseQuery<SellerInspectionDto?>;
