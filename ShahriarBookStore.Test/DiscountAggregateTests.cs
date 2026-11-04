using Domain.Common.Domain.Extention;
using Domain.DiscountAggregate;
using Domain.DiscountAggregate.Enums;
using Xunit;

namespace ShahriarBookStore.Test;

public class DiscountAggregateTests
{
    [Fact]
    public void Create_Discount_With_Valid_Parameters_Should_Succeed()
    {
        // Arrange & Act
        var discount = new Discount(
            code: "SUMMER50",
            discountType: DiscountType.Percentage,
            discountAmount: 50,
            startDate: DateTime.Now.AddDays(-1),
            endDate: DateTime.Now.AddDays(10),
            capacity: 100,
            minOrderAmount: 1000,
            maxDiscountAmount: 50000
        );

        // Assert
        Assert.Equal("SUMMER50", discount.Code);
        Assert.Equal(DiscountType.Percentage, discount.DiscountType);
        Assert.Equal(50, discount.DiscountAmount);
        Assert.True(discount.IsActive);
        Assert.Equal(0, discount.UsedCount);
    }

    [Fact]
    public void Create_Discount_With_Invalid_Percentage_Should_Throw_Exception()
    {
        // Arrange & Act & Assert
        Assert.Throws<InvalidDomainDataException>(() => new Discount(
            code: "INVALID150",
            discountType: DiscountType.Percentage,
            discountAmount: 150,
            startDate: null,
            endDate: null,
            capacity: null,
            minOrderAmount: null,
            maxDiscountAmount: null
        ));
    }

    [Fact]
    public void Create_Discount_With_StartDate_After_EndDate_Should_Throw_Exception()
    {
        // Arrange & Act & Assert
        Assert.Throws<InvalidDomainDataException>(() => new Discount(
            code: "DATEDISCOUNT",
            discountType: DiscountType.FixedAmount,
            discountAmount: 10000,
            startDate: DateTime.Now.AddDays(10),
            endDate: DateTime.Now.AddDays(1),
            capacity: 50,
            minOrderAmount: null,
            maxDiscountAmount: null
        ));
    }

    [Fact]
    public void UseDiscount_When_Capacity_Reached_Should_Throw_Exception()
    {
        // Arrange
        var discount = new Discount(
            code: "LIMITED",
            discountType: DiscountType.FixedAmount,
            discountAmount: 5000,
            startDate: null,
            endDate: null,
            capacity: 1,
            minOrderAmount: null,
            maxDiscountAmount: null
        );

        // Act
        discount.UseDiscount();

        // Assert
        Assert.Equal(1, discount.UsedCount);
        Assert.Throws<InvalidDomainDataException>(() => discount.UseDiscount());
    }

    [Fact]
    public void UseDiscount_When_Deactivated_Should_Throw_Exception()
    {
        // Arrange
        var discount = new Discount(
            code: "INACTIVE",
            discountType: DiscountType.Percentage,
            discountAmount: 20,
            startDate: null,
            endDate: null,
            capacity: 10,
            minOrderAmount: null,
            maxDiscountAmount: null
        );

        discount.Deactivate();

        // Act & Assert
        Assert.False(discount.IsActive);
        Assert.Throws<InvalidDomainDataException>(() => discount.UseDiscount());
    }
}
