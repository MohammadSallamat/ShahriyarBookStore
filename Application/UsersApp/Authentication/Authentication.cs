using MediatR;

namespace Application.UsersApp.Authentication;

public record AuthTokens(string AccessToken, string RefreshToken);
public record AuthSettings(string SecretKey, string Issuer, string Audience);

public interface IOtpSender
{
    Task SendAsync(string phoneNumber, string code, CancellationToken cancellationToken);
}

public interface IAuthenticationService
{
    Task<AuthTokens?> LoginAsync(string phoneNumber, string password, string device, CancellationToken cancellationToken);
    Task RequestOtpAsync(string phoneNumber, CancellationToken cancellationToken);
    Task<AuthTokens?> VerifyOtpAsync(string phoneNumber, string code, string device, CancellationToken cancellationToken);
    Task<AuthTokens?> RefreshAsync(string refreshToken, string device, CancellationToken cancellationToken);
    Task LogoutAsync(string refreshToken, CancellationToken cancellationToken);
    Task<bool> IsAccessTokenActiveAsync(string accessToken, CancellationToken cancellationToken);
}

public record LoginCommand(string PhoneNumber, string Password, string Device) : IRequest<AuthTokens?>;
public record RequestOtpCommand(string PhoneNumber) : IRequest;
public record VerifyOtpCommand(string PhoneNumber, string Code, string Device) : IRequest<AuthTokens?>;
public record RefreshAuthCommand(string RefreshToken, string Device) : IRequest<AuthTokens?>;
public record LogoutCommand(string RefreshToken) : IRequest;

public class AuthenticationCommandHandler : IRequestHandler<LoginCommand, AuthTokens?>,
    IRequestHandler<RequestOtpCommand>, IRequestHandler<VerifyOtpCommand, AuthTokens?>,
    IRequestHandler<RefreshAuthCommand, AuthTokens?>, IRequestHandler<LogoutCommand>
{
    private readonly IAuthenticationService _service;
    public AuthenticationCommandHandler(IAuthenticationService service) => _service = service;
    public Task<AuthTokens?> Handle(LoginCommand r, CancellationToken ct) => _service.LoginAsync(r.PhoneNumber, r.Password, r.Device, ct);
    public Task Handle(RequestOtpCommand r, CancellationToken ct) => _service.RequestOtpAsync(r.PhoneNumber, ct);
    public Task<AuthTokens?> Handle(VerifyOtpCommand r, CancellationToken ct) => _service.VerifyOtpAsync(r.PhoneNumber, r.Code, r.Device, ct);
    public Task<AuthTokens?> Handle(RefreshAuthCommand r, CancellationToken ct) => _service.RefreshAsync(r.RefreshToken, r.Device, ct);
    public Task Handle(LogoutCommand r, CancellationToken ct) => _service.LogoutAsync(r.RefreshToken, ct);
}
