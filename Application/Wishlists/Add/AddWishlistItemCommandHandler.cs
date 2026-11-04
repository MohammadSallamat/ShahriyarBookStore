using Application.CommonApplication;
using Domain.WishlistAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Wishlists.Add;

public class AddWishlistItemCommandHandler : IBaseCommandHandler<AddWishlistItemCommand, long>
{
    private readonly IWishlistRepository _wishlistRepository;

    public AddWishlistItemCommandHandler(IWishlistRepository wishlistRepository)
    {
        _wishlistRepository = wishlistRepository;
    }

    public async Task<OperationResult<long>> Handle(AddWishlistItemCommand request, CancellationToken cancellationToken)
    {
        var exists = await _wishlistRepository.ExistsAsync(w => w.UserId == request.UserId && w.ProductId == request.ProductId);
        if (exists)
            return OperationResult<long>.Error("این محصول قبلاً به لیست علاقه‌مندی‌ها اضافه شده است.");

        var wishlistItem = new Wishlist(request.UserId, request.ProductId);

        await _wishlistRepository.AddAsync(wishlistItem);
        await _wishlistRepository.Save();

        return OperationResult<long>.Success(wishlistItem.Id);
    }
}
