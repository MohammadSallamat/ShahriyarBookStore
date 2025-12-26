using Domain.Common.Domain;
using Domain.Common.Domain.Extention;
using Domain.RoleAggregate.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RoleAggregate;

public class Role : AggregateRoot
{
    public string Title { get; private set; }
    public List<RolePermission> Permissions { get; private set; }

    public Role(string title)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));

        Title = title;
        Permissions = new();
    }

    public void EditTitle(string title)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        Title = title;
    }
    public void AddPermission(Permission permission)
    {
        if (Permissions.Any(p => p.Permission == permission))
            return;

        Permissions.Add(new RolePermission(permission));
    }

    public void RemovePermission(Permission permission)
    {
        var rp = Permissions.FirstOrDefault(p => p.Permission == permission);
        if (rp == null) return;

        Permissions.Remove(rp);
    }
}