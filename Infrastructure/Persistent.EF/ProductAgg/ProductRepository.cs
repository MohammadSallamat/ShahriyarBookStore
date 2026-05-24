using Domain.ProductAggregate;
using Domain.ProductAggregate.Repository;
using Infrastructure._Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistent.EF.ProductAgg
{
    internal class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ShahriarBookStoreContext context) : base(context)
        {
        }
    }
}
