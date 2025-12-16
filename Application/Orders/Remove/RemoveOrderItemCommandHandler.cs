using Application.CommonApplication;
using Domain.OrderAggregate.Repository;

namespace Application.Orders.Remove;

public class RemoveOrderItemCommandHandler : IBaseCommandHandler<RemoveOrderItemCommand>
{
    IOrderRepository _repository;

    public RemoveOrderItemCommandHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(RemoveOrderItemCommand request, CancellationToken cancellationToken)
    {
        var currentOrder = await _repository.GetCurrentUserOrder(request.UserId);
        if (currentOrder == null)
            return OperationResult.NotFound();

        currentOrder.RemoveItem(request.ItemId);
        await _repository.Save();
        return OperationResult.Success();
    }
}

