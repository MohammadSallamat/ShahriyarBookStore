using System.Data;
using System.Security.Cryptography;
using System.Text;
using Application.CommonApplication.SecurityUtil;
using Application.UsersApp.Authentication;
using Domain.UserAggregate;
using Domain.UserAggregate.DomainServices;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistent.EF.UserAgg;

internal sealed class AuthenticationService : IAuthenticationService
{
    private readonly ShahriarBookStoreContext _db;
    private readonly IUserDomainService _domainService;
    private readonly IOtpSender _sender;
    private readonly AuthSettings _settings;
    public AuthenticationService(ShahriarBookStoreContext db, IUserDomainService domainService, IOtpSender sender, AuthSettings settings)
        => (_db, _domainService, _sender, _settings) = (db, domainService, sender, settings);

    private static bool ValidPhone(string? phone) => phone is { Length: 11 } && phone.All(c => c >= '0' && c <= '9');
    private static string Device(string? device) => string.IsNullOrWhiteSpace(device) ? "Unknown Device" : device[..Math.Min(device.Length, 100)];
    private string HashCode(string phone, string code) => Convert.ToHexString(HMACSHA256.HashData(Encoding.UTF8.GetBytes(_settings.SecretKey), Encoding.UTF8.GetBytes(phone + ":" + code)));

    // Transaction-owned SQL Server application locks serialize requests across all app instances.
    private Task LockAsync(string resource, CancellationToken ct) => _db.Database.ExecuteSqlInterpolatedAsync(
        $"DECLARE @result int; EXEC @result = sp_getapplock @Resource={resource}, @LockMode='Exclusive', @LockOwner='Transaction', @LockTimeout=10000; IF @result < 0 THROW 50000, 'Authentication lock unavailable', 1;", ct);

    public async Task<AuthTokens?> LoginAsync(string phoneNumber, string password, string device, CancellationToken ct)
    {
        if (!ValidPhone(phoneNumber) || string.IsNullOrEmpty(password) || password.Length > 1024) return null;
        var user = await _db.Users.AsTracking().Include(x => x.Tokens).SingleOrDefaultAsync(x => x.PhoneNumber.Value == phoneNumber, ct);
        if (user?.Password is null || !user.Password.Verify(password)) return null;
        var tokens = Issue(user, device);
        await _db.SaveChangesAsync(ct);
        return tokens;
    }

    public async Task RequestOtpAsync(string phoneNumber, CancellationToken ct)
    {
        if (!ValidPhone(phoneNumber)) return;
        await using var transaction = await _db.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, ct);
        await LockAsync("otp:" + phoneNumber, ct);
        var challenges = _db.Set<OtpChallenge>();
        var challenge = await challenges.AsTracking().SingleOrDefaultAsync(x => x.PhoneNumber == phoneNumber, ct);
        var now = DateTime.UtcNow;
        if (challenge != null && challenge.LastSentAtUtc.AddSeconds(60) > now) return;
        var code = RandomNumberGenerator.GetInt32(0, 1000000).ToString("D6");
        if (challenge == null)
        {
            challenge = new OtpChallenge { PhoneNumber = phoneNumber };
            challenges.Add(challenge);
        }
        challenge.CodeHash = HashCode(phoneNumber, code);
        challenge.ExpiresAtUtc = now.AddMinutes(5);
        challenge.LastSentAtUtc = now;
        challenge.Attempts = 0;
        challenge.Consumed = false;
        // Sender failure rolls back, and no code is exposed through the response.
        await _sender.SendAsync(phoneNumber, code, ct);
        await _db.SaveChangesAsync(ct);
        await transaction.CommitAsync(ct);
    }

    public async Task<AuthTokens?> VerifyOtpAsync(string phoneNumber, string code, string device, CancellationToken ct)
    {
        if (!ValidPhone(phoneNumber) || code == null || code.Length != 6 || !code.All(c => c >= '0' && c <= '9')) return null;
        await using var transaction = await _db.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, ct);
        await LockAsync("otp:" + phoneNumber, ct);
        var challenge = await _db.Set<OtpChallenge>().AsTracking().SingleOrDefaultAsync(x => x.PhoneNumber == phoneNumber, ct);
        if (challenge == null || challenge.Consumed || challenge.ExpiresAtUtc <= DateTime.UtcNow || challenge.Attempts >= 5) return null;
        challenge.Attempts++;
        var valid = CryptographicOperations.FixedTimeEquals(Convert.FromHexString(challenge.CodeHash), Convert.FromHexString(HashCode(phoneNumber, code)));
        if (!valid)
        {
            await _db.SaveChangesAsync(ct);
            await transaction.CommitAsync(ct);
            return null;
        }
        challenge.Consumed = true;
        var user = await _db.Users.AsTracking().Include(x => x.Tokens).SingleOrDefaultAsync(x => x.PhoneNumber.Value == phoneNumber, ct);
        if (user == null)
        {
            user = User.RegisterPasswordless(phoneNumber, _domainService);
            _db.Users.Add(user);
            // Persist the generated identity before creating JWT claims, in the same transaction.
            await _db.SaveChangesAsync(ct);
        }
        var tokens = Issue(user, device);
        await _db.SaveChangesAsync(ct);
        await transaction.CommitAsync(ct);
        return tokens;
    }

    public async Task<AuthTokens?> RefreshAsync(string refreshToken, string device, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(refreshToken) || refreshToken.Length > 256) return null;
        var hash = JwtTokenBuilder.Sha256Hash(refreshToken);
        await using var transaction = await _db.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, ct);
        await LockAsync("refresh:" + hash, ct);
        var user = await _db.Users.AsTracking().Include(x => x.Tokens).SingleOrDefaultAsync(x => x.Tokens.Any(t => t.HashRefreshToken == hash), ct);
        var token = user?.Tokens.SingleOrDefault(t => t.HashRefreshToken == hash);
        if (token == null || token.RefreshTokenExpireDate <= DateTime.UtcNow) return null;
        user!.RemoveToken(token.Id);
        var result = Issue(user, device);
        await _db.SaveChangesAsync(ct);
        await transaction.CommitAsync(ct);
        return result;
    }

    public async Task LogoutAsync(string refreshToken, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(refreshToken) || refreshToken.Length > 256) return;
        var hash = JwtTokenBuilder.Sha256Hash(refreshToken);
        await using var transaction = await _db.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, ct);
        await LockAsync("refresh:" + hash, ct);
        var user = await _db.Users.AsTracking().Include(x => x.Tokens).SingleOrDefaultAsync(x => x.Tokens.Any(t => t.HashRefreshToken == hash), ct);
        var token = user?.Tokens.SingleOrDefault(t => t.HashRefreshToken == hash);
        if (token != null) { user!.RemoveToken(token.Id); await _db.SaveChangesAsync(ct); }
        await transaction.CommitAsync(ct);
    }

    public Task<bool> IsAccessTokenActiveAsync(string accessToken, CancellationToken ct)
    {
        var hash = JwtTokenBuilder.Sha256Hash(accessToken);
        return _db.Users.AnyAsync(x => x.Tokens.Any(t => t.HashJwtToken == hash && t.TokenExpireDate > DateTime.UtcNow), ct);
    }

    private AuthTokens Issue(User user, string device)
    {
        var token = JwtTokenBuilder.GenerateToken(user.Id, user.Name, user.Family, user.PhoneNumber.Value, _settings.SecretKey, _settings.Issuer, _settings.Audience);
        user.AddToken(JwtTokenBuilder.Sha256Hash(token.AccessToken), JwtTokenBuilder.Sha256Hash(token.RefreshToken), token.TokenExpireDate, token.RefreshTokenExpireDate, Device(device));
        return new AuthTokens(token.AccessToken, token.RefreshToken);
    }
}
