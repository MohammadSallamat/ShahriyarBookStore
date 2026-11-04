using Application.CommonApplication;
using Domain.DiscountAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Discounts.Edit;

public class EditDiscountCommandHandler : IBaseCommandHandler<EditDiscountCommand>
{
    private readonly IDiscountRepository _discountRepository;

    public EditDiscountCommandHandler(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }

    public async Task<OperationResult> Handle(EditDiscountCommand request, CancellationToken cancellationToken)
    {
        var discount = await _discountRepository.GetTracking(request.Id);
        if (discount == null)
            return OperationResult.NotFound("تخفیف مورد نظر یافت نشد.");

        var exists = await _discountRepository.ExistsAsync(d => d.Code == request.Code.Trim().ToUpper() && d.Id != request.Id);
        if (exists)
            return OperationResult.Error("کد تخفیف وارد شده تکراری است.");

        discount.Edit(
            request.Code,
            request.DiscountType,
            request.DiscountAmount,
            request.StartDate,
            request.EndDate,
            request.Capacity,
            request.MinOrderAmount,
            request.MaxDiscountAmount
        );

        await _discountRepository.Save();

        return OperationResult.Success();
    }
}
