using Domain.DiscountAggregate;
using Infrastructure._Utilities;

namespace Infrastructure.Persistent.EF.DiscountAgg;

internal class DiscountRepository : BaseRepository<Discount>, IDiscountRepository
{
    public DiscountRepository(ShahriarBookStoreContext context) : base(context)
    {
    }
}
