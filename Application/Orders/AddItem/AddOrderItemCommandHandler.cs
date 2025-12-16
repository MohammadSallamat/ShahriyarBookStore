using Application.CommonApplication;
using Domain.OrderAggregate;
using Domain.OrderAggregate.Repository;
using Domain.SellerAggregate.Repository;

namespace Application.Orders.AddItem;

public class AddOrderItemCommandHandler : IBaseCommandHandler<AddOrderItemCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ISellerRepository _sellerRepository;

    public AddOrderItemCommandHandler(IOrderRepository orderRepository, ISellerRepository sellerRepository)
    {
        _orderRepository = orderRepository;
        _sellerRepository = sellerRepository;
    }

    public async Task<OperationResult> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
    {
        var inventory=await _sellerRepository.GetInventoryById(request.InventoryId);
        if (inventory == null)
            return  OperationResult.NotFound();
        if (request.Count > inventory.Count)
            return OperationResult.Error("حد درخواستی شما ازموجودی محصول بیشتر است");


        var order= await _orderRepository.GetCurrentUserOrder(request.UserId);
        if (order == null)
        {
            order = new Order(request.UserId);
            order.AddItem(new OrderItem(request.InventoryId,request.Count,inventory.Price),inventory.Count);
            _orderRepository.Add(order);
        }
        else
        {
            order.AddItem(new OrderItem(request.InventoryId, request.Count, inventory.Price),inventory.Count);
        }

        await _orderRepository.Save();
        return OperationResult.Success();

    }
}
