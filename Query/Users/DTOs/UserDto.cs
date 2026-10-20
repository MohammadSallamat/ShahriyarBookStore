using Query.Common.Domain;
using System.Collections.Generic;

namespace Query.Users.DTOs;

public class UserDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public string Family { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string AvatarName { get; set; } = string.Empty;
    public List<UserRoleDto> Roles { get; set; } = new();
}

public class UserRoleDto
{
    public long RoleId { get; set; }
    public string RoleTitle { get; set; } = string.Empty;
}
