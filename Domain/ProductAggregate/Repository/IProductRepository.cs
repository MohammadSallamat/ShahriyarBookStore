using Domain.Common.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProductAggregate.Repository
{
    public interface IProductRepository:IBaseRepository<Product>
    {
    }
}
