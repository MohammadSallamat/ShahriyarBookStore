using Application.CommonApplication;
using Domain.OrderAggregate;
using Domain.OrderAggregate.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.CheckOut;

public class CheckoutOrderCommandHandler : IBaseCommandHandler<CheckoutOrderCommand>
{
    private readonly IOrderRepository _repository;

    public CheckoutOrderCommandHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        var currentOrder = await _repository.GetCurrentUserOrder(request.UserId);
        if (currentOrder == null)
            return OperationResult.NotFound();

        var address = new OrderAddress(request.Shire, request.City, request.PostalCode,
            request.PostalAddress, request.PhoneNumber, request.Name,
            request.Family, request.NationalCode);
        currentOrder.CheckOut(address);

        await _repository.Save();
        return OperationResult.Success();
    }
}
