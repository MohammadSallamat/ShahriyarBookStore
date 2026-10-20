using Query.Common.Domain;
using Query.Users.DTOs;

namespace Query.Users.GetById;

public record GetUserByIdQuery(long UserId) : IBaseQuery<UserDto?>;
