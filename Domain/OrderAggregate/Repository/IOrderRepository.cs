using Domain.Common.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OrderAggregate.Repository
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
    }
}
