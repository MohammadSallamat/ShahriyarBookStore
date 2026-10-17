using Infrastructure.Persistent.EF;
using Microsoft.EntityFrameworkCore;
using Query.Common.Domain;
using Query.Roles.DTOs;

namespace Query.Roles.GetList;

internal class GetRoleListQueryHandler : IBaseQueryHandler<GetRoleListQuery, List<RoleDto>>
{
    private readonly ShahriarBookStoreContext _context;

    public GetRoleListQueryHandler(ShahriarBookStoreContext context)
    {
        _context = context;
    }

    public async Task<List<RoleDto>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
    {
        var roles = await _context.Roles
            .Include(r => r.Permissions)
            .ToListAsync(cancellationToken);

        return roles.Select(role => new RoleDto
        {
            Id = role.Id,
            CreationDate = role.CreationDate,
            Title = role.Title,
            Permissions = role.Permissions?.Select(p => p.Permission).ToList() ?? new()
        }).ToList();
    }
}
