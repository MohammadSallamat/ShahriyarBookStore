using Infrastructure.Persistent.EF;
using Microsoft.EntityFrameworkCore;
using Query.Common.Domain;
using Query.Users.DTOs;

namespace Query.Users.UserTokens.GetByRefreshToken;

internal class GetUserTokenByRefreshTokenQueryHandler : IBaseQueryHandler<GetUserTokenByRefreshTokenQuery, UserTokenDto>
{
    private readonly ShahriarBookStoreContext _context;

    public GetUserTokenByRefreshTokenQueryHandler(ShahriarBookStoreContext context)
    {
        _context = context;
    }

    public async Task<UserTokenDto> Handle(GetUserTokenByRefreshTokenQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(u => u.Tokens)
            .FirstOrDefaultAsync(u => u.Tokens.Any(t => t.HashRefreshToken == request.HashRefreshToken), cancellationToken);

        if (user == null)
            return null;

        var token = user.Tokens.FirstOrDefault(t => t.HashRefreshToken == request.HashRefreshToken);
        if (token == null)
            return null;

        return new UserTokenDto
        {
            Id = token.Id,
            CreationDate = token.CreationDate,
            UserId = token.UserId,
            HashJwtToken = token.HashJwtToken,
            HashRefreshToken = token.HashRefreshToken,
            TokenExpireDate = token.TokenExpireDate,
            RefreshTokenExpireDate = token.RefreshTokenExpireDate,
            Device = token.Device
        };
    }
}
