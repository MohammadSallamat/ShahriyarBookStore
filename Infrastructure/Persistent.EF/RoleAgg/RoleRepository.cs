using Domain.RoleAggregate;
using Domain.RoleAggregate.Repository;
using Infrastructure._Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistent.EF.RoleAgg
{
    internal class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(ShahriarBookStoreContext context) : base(context)
        {
        }
    }
}
