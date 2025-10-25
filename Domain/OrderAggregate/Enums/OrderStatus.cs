using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OrderAggregate.Enums;

public enum OrderStatus
{
    Pennding,
    Finally,
    Shipping,
    Rejected
}
