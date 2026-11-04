using Application.CommonApplication;
using Domain.DiscountAggregate.Enums;
using System;

namespace Application.Discounts.Create;

public record CreateDiscountCommand(
    string Code,
    DiscountType DiscountType,
    int DiscountAmount,
    DateTime? StartDate,
    DateTime? EndDate,
    int? Capacity,
    int? MinOrderAmount,
    int? MaxDiscountAmount
) : IBaseCommand<long>;
