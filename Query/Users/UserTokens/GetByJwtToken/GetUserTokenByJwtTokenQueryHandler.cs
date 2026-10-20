using Infrastructure.Persistent.EF;
using Microsoft.EntityFrameworkCore;
using Query.Common.Domain;
using Query.Users.DTOs;

namespace Query.Users.UserTokens.GetByJwtToken;

internal class GetUserTokenByJwtTokenQueryHandler : IBaseQueryHandler<GetUserTokenByJwtTokenQuery, UserTokenDto>
{
    private readonly ShahriarBookStoreContext _context;

    public GetUserTokenByJwtTokenQueryHandler(ShahriarBookStoreContext context)
    {
        _context = context;
    }

    public async Task<UserTokenDto> Handle(GetUserTokenByJwtTokenQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(u => u.Tokens)
            .FirstOrDefaultAsync(u => u.Tokens.Any(t => t.HashJwtToken == request.HashJwtToken), cancellationToken);

        if (user == null)
            return null;

        var token = user.Tokens.FirstOrDefault(t => t.HashJwtToken == request.HashJwtToken);
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
