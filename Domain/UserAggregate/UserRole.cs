using Domain.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserAggregate;

public class UserRole:BaseEntity
{
    public UserRole(long roleId)
    {
        RoleId=roleId;
    }
    public long UserId { get;internal set; }
    public long RoleId { get; private set; }

}
