using Application.CommonApplication.SecurityUtil;
using Domain.Common.Domain.ValueObjects;
using Domain.UserAggregate;
using Domain.UserAggregate.DomainServices;

namespace ShahriarBookStore.Test;

public class AuthenticationDomainTests
{
    private sealed class NoUsers : IUserDomainService
    {
        public bool PhoneNumberIsExist(string phoneNumber) => false;
        public bool EmailIsExist(string email) => false;
    }

    [Fact]
    public void OtpRegistrationDoesNotCreateAKnownPassword()
    {
        var user = User.RegisterPasswordless("09123456789", new NoUsers());
        Assert.Null(user.Password);
        Assert.Equal("09123456789", user.PhoneNumber.Value);
    }

    [Fact]
    public void ExistingPasswordRegistrationCanStillVerifyPassword()
    {
        var password = Password.Create("correct horse battery staple");
        var user = User.RegisterUser("09123456789", password, new NoUsers());
        Assert.True(user.Password!.Verify("correct horse battery staple"));
        Assert.False(user.Password.Verify("wrong password"));
    }

    [Fact]
    public void GeneratedTokensHaveDistinctRefreshAndAccessTokens()
    {
        const string key = "0123456789012345678901234567890123456789";
        var first = JwtTokenBuilder.GenerateToken(1, "", "", "09123456789", key, "issuer", "audience");
        var second = JwtTokenBuilder.GenerateToken(1, "", "", "09123456789", key, "issuer", "audience");
        Assert.NotEqual(first.RefreshToken, second.RefreshToken);
        Assert.NotEqual(first.AccessToken, second.AccessToken);
    }
}
