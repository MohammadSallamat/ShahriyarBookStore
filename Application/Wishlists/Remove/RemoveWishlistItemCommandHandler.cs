using Application.CommonApplication;
using Domain.WishlistAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Wishlists.Remove;

public class RemoveWishlistItemCommandHandler : IBaseCommandHandler<RemoveWishlistItemCommand>
{
    private readonly IWishlistRepository _wishlistRepository;

    public RemoveWishlistItemCommandHandler(IWishlistRepository wishlistRepository)
    {
        _wishlistRepository = wishlistRepository;
    }

    public async Task<OperationResult> Handle(RemoveWishlistItemCommand request, CancellationToken cancellationToken)
    {
        var wishlistItem = await _wishlistRepository.GetTracking(request.WishlistId);
        if (wishlistItem == null || wishlistItem.UserId != request.UserId)
            return OperationResult.NotFound("آیتم مورد نظر در لیست علاقه‌مندی‌ها یافت نشد.");

        _wishlistRepository.Delete(wishlistItem);
        await _wishlistRepository.Save();

        return OperationResult.Success();
    }
}
