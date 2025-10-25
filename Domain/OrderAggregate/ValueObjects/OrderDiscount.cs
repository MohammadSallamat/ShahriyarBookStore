using Domain.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OrderAggregate.ValueObjects;

public class OrderDiscount : BaseValueObject
{
    public OrderDiscount(string title, int amount)
    {
        Title = title;
        Amount = amount;
    }

    public string Title { get; private set; }
    public int Amount { get; private set; }

}
