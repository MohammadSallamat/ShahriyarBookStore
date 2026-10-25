using Application.CommonApplication;
using Microsoft.AspNetCore.Mvc;

namespace Facade.Presentation._Utils;

public static class ApiControllerExtensions
{
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

    public static IActionResult ToActionResult<TData>(this OperationResult<TData> result)
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
