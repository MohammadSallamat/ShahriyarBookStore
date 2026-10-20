using Infrastructure.Persistent.EF;
using Microsoft.EntityFrameworkCore;
using Query.Common.Domain;
using Query.Users.DTOs;

namespace Query.Users.GetById;

internal class GetUserByIdQueryHandler : IBaseQueryHandler<GetUserByIdQuery, UserDto>
{
    private readonly ShahriarBookStoreContext _context;

    public GetUserByIdQueryHandler(ShahriarBookStoreContext context)
    {
        _context = context;
    }

    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        if (user == null)
            return null;

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
        };
    }
}
