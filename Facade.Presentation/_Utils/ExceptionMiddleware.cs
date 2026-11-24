using System.Net;
using System.Net.NetworkInformation;
using System.Text.Json;
using Domain.Common.Domain.Exception;
using Domain.Common.Domain.Extention;


namespace Facade.Presentation._Utils;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            // اگر مشکلی نباشه، درخواست رو بفرست جلو
            await _next(context);
        }
        catch (InvalidDomainDataException ex) 
        {
            // ارورهای دومین رو تبدیل به کد 400 (Bad Request) می‌کنه
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";

            var response = new
            {
                Message = ex.Message,
                Status = 400
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        catch (DuplicatePhoneNumberDomainException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Conflict;
            context.Response.ContentType = "application/json";

            var response = new
            {
                Message = ex.Message,
                Status = 409
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        catch (DuplicateAddressDomainException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Conflict;
            context.Response.ContentType = "application/json";

            var response = new
            {
                Message = ex.Message,
                Status = 409
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        catch (DuplicateEmailDomainException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Conflict;
            context.Response.ContentType = "application/json";

            var response = new
            {
                Message = ex.Message,
                Status = 409
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled request failure");
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync(new { Message = "An unexpected error occurred." });
        }
    }
}