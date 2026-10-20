using Query.Common.Domain;
using Query.Users.DTOs;

namespace Query.Users.GetByFilter;

public record GetUsersByFilterQuery(UserFilterParams FilterParams) : IBaseQuery<UserFilterResult>;
