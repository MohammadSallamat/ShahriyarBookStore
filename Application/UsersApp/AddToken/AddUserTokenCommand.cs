using Application.CommonApplication;
using System;

namespace Application.UsersApp.AddToken;

public record AddUserTokenCommand(
    long UserId,
    string HashJwtToken,
    string HashRefreshToken,
    DateTime TokenExpireDate,
    DateTime RefreshTokenExpireDate,
    string Device
) : IBaseCommand;
