using Domain.UserAggregate.Enums;
using Query.Common.Domain;

namespace Query.Users.DTOs;

public class UserDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public string Family { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? AvatarName { get; set; }
    public Gender Gender { get; set; }
    public List<UserRoleDto> Roles { get; set; } = new();
    public List<UserAddressDto> Addresses { get; set; } = new();
}

public class UserRoleDto : BaseDto
{
    public long UserId { get; set; }
    public long RoleId { get; set; }
    public string RoleTitle { get; set; } = string.Empty;
}

public class UserAddressDto : BaseDto
{
    public long UserId { get; set; }
    public string Shire { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string PostalAddress { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Family { get; set; } = string.Empty;
    public string NationalCode { get; set; } = string.Empty;
    public bool ActiveAddress { get; set; }
}

public class UserFilterParams
{
    public string? Search { get; set; }
    public string? PhoneNumber { get; set; }
    public int PageId { get; set; } = 1;
    public int Take { get; set; } = 10;
}

public class UserFilterResult
{
    public List<UserDto> Users { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageId { get; set; }
    public int Take { get; set; }
    public int PageCount => (int)Math.Ceiling((double)TotalCount / (Take == 0 ? 1 : Take));
}
