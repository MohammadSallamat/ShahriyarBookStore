using Application.CommonApplication;

namespace Application.Discounts.Delete;

public record DeleteDiscountCommand(long DiscountId) : IBaseCommand;
