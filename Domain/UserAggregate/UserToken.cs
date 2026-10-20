using Domain.Common.Domain;
using Domain.Common.Domain.Exception;
using Domain.Common.Domain.Extention;
using System;

namespace Domain.UserAggregate;

public class UserToken : BaseEntity
{
    private UserToken()
    {

    }

    public UserToken(string hashJwtToken, string hashRefreshToken, DateTime tokenExpireDate, DateTime refreshTokenExpireDate, string device)
    {
        NullOrEmptyDomainDataException.CheckString(hashJwtToken, nameof(hashJwtToken));
        NullOrEmptyDomainDataException.CheckString(hashRefreshToken, nameof(hashRefreshToken));
        NullOrEmptyDomainDataException.CheckString(device, nameof(device));

        if (tokenExpireDate < DateTime.Now)
            throw new InvalidDomainDataException("Token Expire Date is invalid");

        if (refreshTokenExpireDate < DateTime.Now)
            throw new InvalidDomainDataException("Refresh Token Expire Date is invalid");

        HashJwtToken = hashJwtToken;
        HashRefreshToken = hashRefreshToken;
        TokenExpireDate = tokenExpireDate;
        RefreshTokenExpireDate = refreshTokenExpireDate;
        Device = device;
    }

    public long UserId { get; internal set; }
    public string HashJwtToken { get; private set; }
    public string HashRefreshToken { get; private set; }
    public DateTime TokenExpireDate { get; private set; }
    public DateTime RefreshTokenExpireDate { get; private set; }
    public string Device { get; private set; }
}
