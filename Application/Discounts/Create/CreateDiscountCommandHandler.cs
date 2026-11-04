using Application.CommonApplication;
using Domain.DiscountAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Discounts.Create;

public class CreateDiscountCommandHandler : IBaseCommandHandler<CreateDiscountCommand, long>
{
    private readonly IDiscountRepository _discountRepository;

    public CreateDiscountCommandHandler(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }

    public async Task<OperationResult<long>> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
    {
        var exists = await _discountRepository.ExistsAsync(d => d.Code == request.Code.Trim().ToUpper());
        if (exists)
            return OperationResult<long>.Error("کد تخفیف وارد شده تکراری است.");

        var discount = new Discount(
            request.Code,
            request.DiscountType,
            request.DiscountAmount,
            request.StartDate,
            request.EndDate,
            request.Capacity,
            request.MinOrderAmount,
            request.MaxDiscountAmount
        );

        await _discountRepository.AddAsync(discount);
        await _discountRepository.Save();

        return OperationResult<long>.Success(discount.Id);
    }
}
