using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.CommonApplication.SecurityUtil;
using Application.UsersApp.Authentication;
using Domain.UserAggregate.DomainServices;
using Infrastructure;
using Infrastructure.Persistent.EF;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ShahriarBookStore.Test;

public class AuthenticationSecurityTests
{
    private const string Secret = "0123456789012345678901234567890123456789";
    private sealed class NoUsers : IUserDomainService
    {
        public bool PhoneNumberIsExist(string phone) => false;
        public bool EmailIsExist(string email) => false;
    }
    private sealed class NoSender : IOtpSender
    {
        public Task SendAsync(string phoneNumber, string code, CancellationToken cancellationToken) => throw new Exception("Must not send");
    }
    private static ServiceProvider Services()
    {
        var services = new ServiceCollection();
        services.Init("Server=localhost;Database=notused;User Id=sa;Password=notused;TrustServerCertificate=True");
        services.AddSingleton<IUserDomainService, NoUsers>();
        services.AddSingleton<IOtpSender, NoSender>();
        services.AddSingleton(new AuthSettings(Secret, "issuer", "audience"));
        return services.BuildServiceProvider();
    }

    [Theory]
    [InlineData("1234567890a")]
    [InlineData("۱۲۳۴۵۶۷۸۹۰۱")]
    [InlineData("1234567890")]
    public async Task InvalidPhonesAreRejectedBeforeDatabaseOrDelivery(string phone)
    {
        using var provider = Services();
        var auth = provider.GetRequiredService<IAuthenticationService>();
        await auth.RequestOtpAsync(phone, default);
        Assert.Null(await auth.VerifyOtpAsync(phone, "123456", "device", default));
        Assert.Null(await auth.LoginAsync(phone, "password", "device", default));
    }

    [Fact]
    public async Task InvalidSecretsAreRejectedBeforeDatabaseAccess()
    {
        using var provider = Services();
        var auth = provider.GetRequiredService<IAuthenticationService>();
        Assert.Null(await auth.VerifyOtpAsync("09123456789", "１２３４５６", "device", default));
        Assert.Null(await auth.LoginAsync("09123456789", new string('a', 1025), "device", default));
        Assert.Null(await auth.RefreshAsync(new string('a', 257), "device", default));
        await auth.LogoutAsync(new string('a', 257), default);
    }

    [Fact]
    public void IssuedJwtIsSignedAndBoundToIssuerAudienceAndUser()
    {
        var result = JwtTokenBuilder.GenerateToken(42, "", "", "09123456789", Secret, "issuer", "audience");
        var handler = new JwtSecurityTokenHandler();
        var parameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret)),
            ValidateIssuerSigningKey = true, ValidateIssuer = true, ValidIssuer = "issuer",
            ValidateAudience = true, ValidAudience = "audience", ValidateLifetime = true, ClockSkew = TimeSpan.Zero
        };
        var principal = handler.ValidateToken(result.AccessToken, parameters, out _);
        Assert.Equal("42", principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        Assert.Equal(DateTimeKind.Utc, result.TokenExpireDate.Kind);
        parameters.ValidAudience = "wrong";
        Assert.Throws<SecurityTokenInvalidAudienceException>(() => handler.ValidateToken(result.AccessToken, parameters, out _));
    }

    [Fact]
    public void AuthenticationMigrationCanGenerateSqlWithoutDatabase()
    {
        using var provider = Services();
        var db = provider.GetRequiredService<ShahriarBookStoreContext>();
        var script = db.GetService<IMigrator>().GenerateScript();
        Assert.Contains("CREATE TABLE [user].[OtpChallenges]", script);
        Assert.Contains("CREATE TABLE [user].[Tokens]", script);
        Assert.Contains("IX_Users_PhoneNumber", script);
    }
}
