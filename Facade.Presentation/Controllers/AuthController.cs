using Application.CommonApplication;
using Application.UsersApp.Authentication;
using Facade.Presentation.Config;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Facade.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator) => _mediator = mediator;
    private string Device => Request.Headers.UserAgent.ToString();

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto dto, CancellationToken ct)
    {
        var result = await _mediator.Send(new LoginCommand(dto.PhoneNumber, dto.Password, Device), ct);
        return result == null ? BadRequest(OperationResult.Error("اطلاعات ورود نامعتبر است.")) : Ok(result);
    }

    [HttpPost("request-otp")]
    public async Task<IActionResult> RequestOtp([FromBody] OtpRequestDto dto, CancellationToken ct)
    {
        try { await _mediator.Send(new RequestOtpCommand(dto.PhoneNumber), ct); }
        catch (OtpDeliveryUnavailableException) { return StatusCode(503, new { message = "OTP delivery is unavailable." }); }
        return Ok(new { message = "در صورت امکان، کد ارسال خواهد شد." });
    }

    [HttpPost("verify-otp")]
    public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpRequestDto dto, CancellationToken ct)
    {
        var result = await _mediator.Send(new VerifyOtpCommand(dto.PhoneNumber, dto.Code, Device), ct);
        return result == null ? BadRequest(OperationResult.Error("کد نامعتبر یا منقضی شده است.")) : Ok(result);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto dto, CancellationToken ct)
    {
        var result = await _mediator.Send(new RefreshAuthCommand(dto.RefreshToken, Device), ct);
        return result == null ? BadRequest(OperationResult.Error("توکن بازیابی نامعتبر یا منقضی شده است.")) : Ok(result);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutRequestDto dto, CancellationToken ct)
    {
        await _mediator.Send(new LogoutCommand(dto.RefreshToken), ct);
        return Ok(OperationResult.Success());
    }
}

public class LoginRequestDto { public string PhoneNumber { get; set; } = string.Empty; public string Password { get; set; } = string.Empty; }
public class OtpRequestDto { public string PhoneNumber { get; set; } = string.Empty; }
public class VerifyOtpRequestDto { public string PhoneNumber { get; set; } = string.Empty; public string Code { get; set; } = string.Empty; }
public class RefreshTokenRequestDto { public string RefreshToken { get; set; } = string.Empty; }
public class LogoutRequestDto { public string RefreshToken { get; set; } = string.Empty; }
