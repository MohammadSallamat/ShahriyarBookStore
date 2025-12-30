using Application.CommonApplication;
using Domain.SellerAggregate.Repository;

namespace Application.Sellers.AddInventory;

public class AddSellerInventoryCommandHandler : IBaseCommandHandler<AddSellerInventoryCommand>
{
    private readonly ISellerRepository _sellerRepository;

    public AddSellerInventoryCommandHandler(ISellerRepository sellerRepository)
    {
        _sellerRepository = sellerRepository;
    }

    public async Task<OperationResult> Handle(AddSellerInventoryCommand request, CancellationToken cancellationToken)
    {
        var sellerInvntory = await _sellerRepository.GetTracking(request.SellerId);
        if (sellerInvntory == null)
            return OperationResult.NotFound();

        sellerInvntory.AddInventory(new Domain.SellerAggregate.SellerInventory
            (request.ProductId,request.Count,request.Price));

        await _sellerRepository.Save();
        return OperationResult.Success();
    }
}

