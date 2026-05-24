using Domain.UserAggregate;
using Domain.UserAggregate.Repository;
using Infrastructure._Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistent.EF.UserAgg;

internal class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ShahriarBookStoreContext context) : base(context)
    {
    }
}
