using Domain.UserAggregate;
using Infrastructure.Persistent.EF;
using Microsoft.EntityFrameworkCore;
using Query.Common.Domain;
using Query.Users.DTOs;

namespace Query.Users.GetById;

internal class GetUserByIdQueryHandler : IBaseQueryHandler<GetUserByIdQuery, UserDto>
public class GetUserByIdQueryHandler : IBaseQueryHandler<GetUserByIdQuery, UserDto?>
{
    private readonly ShahriarBookStoreContext _context;

    public GetUserByIdQueryHandler(ShahriarBookStoreContext context)
    {
        _context = context;
    }

    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(u => u.Roles)
            .Include(u => u.UserAddresses)
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        if (user == null)
            return null;

        return MapUser(user);
    }

    public static UserDto MapUser(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            CreationDate = user.CreationDate,
            Name = user.Name,
            Family = user.Family,
            PhoneNumber = user.PhoneNumber.Value,
            Email = user.Email,
            AvatarName = user.AvatarName ?? "avatar.png",
            Roles = user.Roles?.Select(r => new UserRoleDto
            {
                RoleId = r.RoleId,
                RoleTitle = ""
            }).ToList() ?? new()
            PhoneNumber = user.PhoneNumber?.Value ?? string.Empty,
            Email = user.Email,
            AvatarName = user.AvatarName,
            Gender = user.Gender,
            Roles = user.Roles.Select(r => new UserRoleDto
            {
                Id = r.Id,
                UserId = r.UserId,
                RoleId = r.RoleId
            }).ToList(),
            Addresses = user.UserAddresses.Select(a => new UserAddressDto
            {
                Id = a.Id,
                UserId = a.UserId,
                Shire = a.Province,
                City = a.City,
                PostalCode = a.PostalCode,
                PostalAddress = a.PostalAddress,
                PhoneNumber = a.PhoneNumber?.Value ?? string.Empty,
                Name = a.Name,
                Family = a.Family,
                NationalCode = a.NationalCode,
                ActiveAddress = a.ISActive
            }).ToList()
        };
    }
}
