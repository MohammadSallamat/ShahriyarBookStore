using Application.CommonApplication;
using Domain.SellerAggregate.Repository;

namespace Application.Sellers.ChangeStatus;

public class ChangeSellerStatusCommandHandler : IBaseCommandHandler<ChangeSellerStatusCommand>
{
    private readonly ISellerRepository _repository;

    public ChangeSellerStatusCommandHandler(ISellerRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(ChangeSellerStatusCommand request, CancellationToken cancellationToken)
    {
        var seller = await _repository.GetTracking(request.SellerId);
        if (seller == null)
            return OperationResult.NotFound();

        seller.ChangeStatus(request.Status);
        await _repository.Save();
        return OperationResult.Success();
    }
}
