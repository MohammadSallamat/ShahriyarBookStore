using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.CommonApplication.SecurityUtil;

public record JwtTokenResult(
    string AccessToken,
    string RefreshToken,
    DateTime TokenExpireDate,
    DateTime RefreshTokenExpireDate
);

public static class JwtTokenBuilder
{
    public static JwtTokenResult GenerateToken(long userId, string name, string family, string phoneNumber, string secretKey, string issuer, string audience)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(secretKey);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userId.ToString()),
            new(ClaimTypes.Name, $"{name} {family}".Trim()),
            new(ClaimTypes.MobilePhone, phoneNumber),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var tokenExpireDate = DateTime.UtcNow.AddDays(1);
        var refreshTokenExpireDate = DateTime.UtcNow.AddDays(30);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = tokenExpireDate,
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var accessToken = tokenHandler.WriteToken(token);
        var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

        return new JwtTokenResult(accessToken, refreshToken, tokenExpireDate, refreshTokenExpireDate);
    }

    public static string Sha256Hash(string input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}
