using Query.Common.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Users.List_of_users;

public record GetUserListQuery : IBaseQuery<IEnumerable<UserListItemDto>>;

