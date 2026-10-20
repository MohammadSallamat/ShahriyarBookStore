using Application.CommonApplication;
using Application.CommonApplication.SecurityUtil;
using Application.UsersApp.AddToken;
using Application.UsersApp.RemoveToken;
using Facade.Presentation._Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Query.Users.GetById;
using Query.Users.GetByPhoneNumber;
using Query.Users.UserTokens.GetByRefreshToken;
using System;
using System.Threading.Tasks;

namespace Facade.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;

    public AuthController(IMediator mediator, IConfiguration configuration)
    {
        _mediator = mediator;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
    {
        var user = await _mediator.Send(new GetUserByPhoneNumberQuery(loginDto.PhoneNumber));

        if (user == null)
            return BadRequest(OperationResult.Error("کاربری با این مشخصات یافت نشد."));

        var secretKey = _configuration["Jwt:SecretKey"] ?? "ShahriarBookStore_SecretKey_2026_JWT_Token_Key_Long";
        var issuer = _configuration["Jwt:Issuer"] ?? "ShahriarBookStore";
        var audience = _configuration["Jwt:Audience"] ?? "ShahriarBookStoreClient";

        var tokenResult = JwtTokenBuilder.GenerateToken(user.Id, user.Name, user.Family, user.PhoneNumber, secretKey, issuer, audience);
        var hashJwt = JwtTokenBuilder.Sha256Hash(tokenResult.AccessToken);
        var hashRefreshToken = JwtTokenBuilder.Sha256Hash(tokenResult.RefreshToken);

        var device = Request.Headers["User-Agent"].ToString();
        if (string.IsNullOrEmpty(device))
            device = "Unknown Device";

        var addTokenCommand = new AddUserTokenCommand(
            user.Id,
            hashJwt,
            hashRefreshToken,
            tokenResult.TokenExpireDate,
            tokenResult.RefreshTokenExpireDate,
            device
        );

        var result = await _mediator.Send(addTokenCommand);
        if (result.Status != OperationResultStatus.Success)
            return BadRequest(result);

        return Ok(new
        {
            accessToken = tokenResult.AccessToken,
            refreshToken = tokenResult.RefreshToken
        });
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto dto)
    {
        var hashRefreshToken = JwtTokenBuilder.Sha256Hash(dto.RefreshToken);
        var tokenDto = await _mediator.Send(new GetUserTokenByRefreshTokenQuery(hashRefreshToken));

        if (tokenDto == null || tokenDto.RefreshTokenExpireDate < DateTime.Now)
            return BadRequest(OperationResult.Error("توکن بازیابی نامعتبر یا منقضی شده است."));

        var user = await _mediator.Send(new GetUserByIdQuery(tokenDto.UserId));
        if (user == null)
            return BadRequest(OperationResult.Error("کاربر یافت نشد."));

        var secretKey = _configuration["Jwt:SecretKey"] ?? "ShahriarBookStore_SecretKey_2026_JWT_Token_Key_Long";
        var issuer = _configuration["Jwt:Issuer"] ?? "ShahriarBookStore";
        var audience = _configuration["Jwt:Audience"] ?? "ShahriarBookStoreClient";

        var tokenResult = JwtTokenBuilder.GenerateToken(user.Id, user.Name, user.Family, user.PhoneNumber, secretKey, issuer, audience);
        var hashJwt = JwtTokenBuilder.Sha256Hash(tokenResult.AccessToken);
        var newHashRefreshToken = JwtTokenBuilder.Sha256Hash(tokenResult.RefreshToken);

        await _mediator.Send(new RemoveUserTokenCommand(user.Id, tokenDto.Id));

        var device = Request.Headers["User-Agent"].ToString();
        if (string.IsNullOrEmpty(device))
            device = "Unknown Device";

        await _mediator.Send(new AddUserTokenCommand(
            user.Id,
            hashJwt,
            newHashRefreshToken,
            tokenResult.TokenExpireDate,
            tokenResult.RefreshTokenExpireDate,
            device
        ));

        return Ok(new
        {
            accessToken = tokenResult.AccessToken,
            refreshToken = tokenResult.RefreshToken
        });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutRequestDto dto)
    {
        var hashRefreshToken = JwtTokenBuilder.Sha256Hash(dto.RefreshToken);
        var tokenDto = await _mediator.Send(new GetUserTokenByRefreshTokenQuery(hashRefreshToken));

        if (tokenDto == null)
            return Ok();

        await _mediator.Send(new RemoveUserTokenCommand(tokenDto.UserId, tokenDto.Id));
        return Ok(OperationResult.Success());
    }
}

public class LoginRequestDto
{
    public string PhoneNumber { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class RefreshTokenRequestDto
{
    public string RefreshToken { get; set; } = string.Empty;
}

public class LogoutRequestDto
{
    public string RefreshToken { get; set; } = string.Empty;
}
