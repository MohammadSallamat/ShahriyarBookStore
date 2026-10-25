using Domain.Common.Domain.Extention;
using Domain.WishlistAggregate;
using Xunit;

namespace ShahriarBookStore.Test;

public class WishlistAggregateTests
{
    [Fact]
    public void Create_Wishlist_With_Valid_Parameters_Should_Succeed()
    {
        // Arrange & Act
        var wishlist = new Wishlist(userId: 10, productId: 20);

        // Assert
        Assert.Equal(10, wishlist.UserId);
        Assert.Equal(20, wishlist.ProductId);
    }

    [Fact]
    public void Create_Wishlist_With_Invalid_UserId_Should_Throw_Exception()
    {
        // Arrange & Act & Assert
        Assert.Throws<InvalidDomainDataException>(() => new Wishlist(userId: 0, productId: 20));
    }

    [Fact]
    public void Create_Wishlist_With_Invalid_ProductId_Should_Throw_Exception()
    {
        // Arrange & Act & Assert
        Assert.Throws<InvalidDomainDataException>(() => new Wishlist(userId: 10, productId: -1));
    }
}
