using Domain.Common.Domain;
using Domain.RoleAggregate.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RoleAggregate;

public class RolePermission : BaseEntity
{
    public RolePermission(Permission permission)
    {
        Permission = permission;
    }
    public long RoleId { get; internal set; }
    public Permission Permission { get; private set; }
}
