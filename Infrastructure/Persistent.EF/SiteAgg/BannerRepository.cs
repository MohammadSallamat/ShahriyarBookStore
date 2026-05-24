using Domain.SiteEntities;
using Domain.SiteEntities.Repositories;
using Infrastructure._Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistent.EF.SiteAgg;

internal class BannerRepository:BaseRepository<Banner>,IBannerRepository
{
    public BannerRepository(ShahriarBookStoreContext context) : base(context)
    {
    }
}
