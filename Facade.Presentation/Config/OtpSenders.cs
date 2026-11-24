using Application.UsersApp.Authentication;

namespace Facade.Presentation.Config;

public sealed class DevelopmentOtpSender : IOtpSender
{
    private readonly ILogger<DevelopmentOtpSender> _logger;
    public DevelopmentOtpSender(ILogger<DevelopmentOtpSender> logger) => _logger = logger;
    public Task SendAsync(string phoneNumber, string code, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Development OTP for {PhoneNumber}: {Code}", phoneNumber, code);
        return Task.CompletedTask;
    }
}

public sealed class UnavailableOtpSender : IOtpSender
{
    public Task SendAsync(string phoneNumber, string code, CancellationToken cancellationToken)
        => throw new OtpDeliveryUnavailableException();
}

public sealed class OtpDeliveryUnavailableException : Exception { }
