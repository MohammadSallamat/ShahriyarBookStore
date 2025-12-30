using Application.CommonApplication;
using Domain.SellerAggregate.Repository;

namespace Application.Sellers.EditInventory;

public class EditSellerInventoryCommandHandler : IBaseCommandHandler<EditSellerInventoryCommand>
{

    private readonly ISellerRepository _sellerRepository;

    public EditSellerInventoryCommandHandler(ISellerRepository sellerRepository)
    {
        _sellerRepository = sellerRepository;
    }

    public async Task<OperationResult> Handle(EditSellerInventoryCommand request, CancellationToken cancellationToken)
    {
        var sellerInvntory = await _sellerRepository.GetTracking(request.SellerId);
        if (sellerInvntory == null)
            return OperationResult.NotFound();

        sellerInvntory.EditInventory(request.inventoryId, request.Count, request.Price,request.DiscountPercentage);

        await _sellerRepository.Save();
        return OperationResult.Success();
    }
}
