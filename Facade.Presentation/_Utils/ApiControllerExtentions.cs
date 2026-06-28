
using Application.CommonApplication;
using Microsoft.AspNetCore.Mvc;

namespace Facade.Presentation._Utils;
public static class ApiControllerExtensions
{
    // این متد خودش تشخیص میده Status چیه و همون کد HTTP رو برمی‌گردونه
    public static IActionResult ToActionResult(this OperationResult result)
    {
        return result.Status switch
        {
            OperationResultStatus.Success => new OkObjectResult(result),
            OperationResultStatus.NotFound => new NotFoundObjectResult(result),
            OperationResultStatus.Error => new BadRequestObjectResult(result),
            _ => new ObjectResult(result) { StatusCode = 500 }
        };
    }
}