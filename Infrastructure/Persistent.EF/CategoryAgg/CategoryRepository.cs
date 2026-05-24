using Domain.CategoryAggregate;
using Domain.Common.Domain.Repository;
using Infrastructure._Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistent.EF.CategoryAgg;

internal class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ShahriarBookStoreContext context) : base(context)
    {
    }

}
