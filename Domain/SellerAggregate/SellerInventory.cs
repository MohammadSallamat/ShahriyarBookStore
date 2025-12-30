using Domain.Common.Domain;
using Domain.Common.Domain.Extention;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SellerAggregate;

public class SellerInventory:BaseEntity
{
    public SellerInventory(long productId, int count, int price, int? discountPercentage = null)
    {
        if (price < 1000 || count < 0)
            throw new InvalidDomainDataException();

        ProductId = productId;
        Count = count;
        Price = price;
        DiscountPercentage = discountPercentage;
    }

    public long SellerId { get; internal set; }
    public long ProductId { get; private set; }
    public int Count { get; private set; }
    public int Price { get; private set; }
    public int? DiscountPercentage { get; private set; }


    public void Edit(int count, int price, int? discountPercentage = null)
    {
        if (price < 1000 || count < 0)
            throw new InvalidDomainDataException();

        Count = count;
        Price = price;
        DiscountPercentage = discountPercentage;
    }
}
