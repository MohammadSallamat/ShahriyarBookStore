using Domain.SiteEntities;
using Domain.SiteEntities.Repositories;
using Infrastructure._Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistent.EF.SiteAgg;

internal class SliderRepository : BaseRepository<Slider>, ISliderRepository
{
    public SliderRepository(ShahriarBookStoreContext context) : base(context)
    {
    }
}
