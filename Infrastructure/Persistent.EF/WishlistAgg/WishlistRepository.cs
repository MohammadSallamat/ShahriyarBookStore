using Domain.WishlistAggregate;
using Infrastructure._Utilities;

namespace Infrastructure.Persistent.EF.WishlistAgg;

internal class WishlistRepository : BaseRepository<Wishlist>, IWishlistRepository
{
    public WishlistRepository(ShahriarBookStoreContext context) : base(context)
    {
    }
}
