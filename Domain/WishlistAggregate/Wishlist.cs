using Domain.Common.Domain;
using Domain.Common.Domain.Extention;

namespace Domain.WishlistAggregate;

public class Wishlist : AggregateRoot
{
    public long UserId { get; private set; }
    public long ProductId { get; private set; }

    private Wishlist()
    {
    }

    public Wishlist(long userId, long productId)
    {
        if (userId <= 0)
            throw new InvalidDomainDataException("شناسه کاربر نامعتبر است.");

        if (productId <= 0)
            throw new InvalidDomainDataException("شناسه محصول نامعتبر است.");

        UserId = userId;
        ProductId = productId;
    }
}
