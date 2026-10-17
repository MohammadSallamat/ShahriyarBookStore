using Infrastructure.Persistent.EF;
using Microsoft.EntityFrameworkCore;
using Query.Common.Domain;
using Query.Roles.DTOs;

namespace Query.Roles.GetById;

internal class GetRoleByIdQueryHandler : IBaseQueryHandler<GetRoleByIdQuery, RoleDto>
{
    private readonly ShahriarBookStoreContext _context;

    public GetRoleByIdQueryHandler(ShahriarBookStoreContext context)
    {
        _context = context;
    }

    public async Task<RoleDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _context.Roles
            .Include(r => r.Permissions)
            .FirstOrDefaultAsync(r => r.Id == request.RoleId, cancellationToken);

        if (role == null)
            return null;

        return new RoleDto
        {
            Id = role.Id,
            CreationDate = role.CreationDate,
            Title = role.Title,
            Permissions = role.Permissions?.Select(p => p.Permission).ToList() ?? new()
        };
    }
}
