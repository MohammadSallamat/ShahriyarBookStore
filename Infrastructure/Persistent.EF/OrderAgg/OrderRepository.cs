using Domain.OrderAggregate;
using Domain.OrderAggregate.Repository;
using Infrastructure._Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistent.EF.OrderAgg
{
    internal class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(ShahriarBookStoreContext context) : base(context)
        {

        }

        public async Task<Order?> GetCurrentUserOrder(long userId)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.UserId == userId);
        }
    }
}
