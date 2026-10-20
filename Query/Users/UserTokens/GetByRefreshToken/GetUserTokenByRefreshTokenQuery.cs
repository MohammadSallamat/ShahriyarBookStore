using Query.Common.Domain;
using Query.Users.DTOs;

namespace Query.Users.UserTokens.GetByRefreshToken;

public record GetUserTokenByRefreshTokenQuery(string HashRefreshToken) : IBaseQuery<UserTokenDto>;
