using Application.CommonApplication;
using Domain.DiscountAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Discounts.Delete;

public class DeleteDiscountCommandHandler : IBaseCommandHandler<DeleteDiscountCommand>
{
    private readonly IDiscountRepository _discountRepository;

    public DeleteDiscountCommandHandler(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }

    public async Task<OperationResult> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
    {
        var discount = await _discountRepository.GetTracking(request.DiscountId);
        if (discount == null)
            return OperationResult.NotFound("تخفیف مورد نظر یافت نشد.");

        discount.Deactivate();
        await _discountRepository.Save();

        return OperationResult.Success();
    }
}
