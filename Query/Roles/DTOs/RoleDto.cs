using Domain.RoleAggregate.Enums;
using Query.Common.Domain;
using System.Collections.Generic;

namespace Query.Roles.DTOs;

public class RoleDto : BaseDto
{
    public string Title { get; set; } = string.Empty;
    public List<Permission> Permissions { get; set; } = new();
}
