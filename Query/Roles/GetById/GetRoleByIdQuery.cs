using Query.Common.Domain;
using Query.Roles.DTOs;

namespace Query.Roles.GetById;

public record GetRoleByIdQuery(long RoleId) : IBaseQuery<RoleDto>;
