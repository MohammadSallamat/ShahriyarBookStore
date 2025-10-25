using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OrderAggregate.ValueObjects;

public class ShippingMethod
{
    public ShippingMethod(string shippingType, int shippingCost)
    {
        ShippingType = shippingType;
        ShippingCost = shippingCost;
    }

    public string ShippingType { get; private set; }
    public int ShippingCost { get; private set; }
}
