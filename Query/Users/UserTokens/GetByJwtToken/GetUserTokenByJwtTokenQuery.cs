using Query.Common.Domain;
using Query.Users.DTOs;

namespace Query.Users.UserTokens.GetByJwtToken;

public record GetUserTokenByJwtTokenQuery(string HashJwtToken) : IBaseQuery<UserTokenDto>;
