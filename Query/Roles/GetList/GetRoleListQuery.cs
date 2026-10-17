using Query.Common.Domain;
using Query.Roles.DTOs;
using System.Collections.Generic;

namespace Query.Roles.GetList;

public record GetRoleListQuery : IBaseQuery<List<RoleDto>>;
