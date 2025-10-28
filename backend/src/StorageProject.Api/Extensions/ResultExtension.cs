using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;

namespace StorageProject.Api.Extensions
{
    public static class ResultExtension
    {
        public static IActionResult ToActionResult(this Result result)
        {
            return result.Status switch
            {
                ResultStatus.Ok => new OkObjectResult(result),
                ResultStatus.Created => new OkObjectResult(result),
                ResultStatus.Invalid => new BadRequestObjectResult(result),
                ResultStatus.NotFound => new NotFoundObjectResult(result),
                ResultStatus.Conflict => new ConflictObjectResult(result),
                ResultStatus.Unauthorized => new UnauthorizedObjectResult(result),
                ResultStatus.Forbidden => new ObjectResult(result) { StatusCode = StatusCodes.Status403Forbidden },
                ResultStatus.Error => new ObjectResult(result) { StatusCode = StatusCodes.Status400BadRequest}
            };
        }
        public static IActionResult ToActionResult<T>(this Result<T> result)
        {
            return result.Status switch
            {
                ResultStatus.Ok => new OkObjectResult(result),
                ResultStatus.Created => new OkObjectResult(result),
                ResultStatus.Invalid => new BadRequestObjectResult(result),
                ResultStatus.NotFound => new NotFoundObjectResult(result),
                ResultStatus.Conflict => new ConflictObjectResult(result),
                ResultStatus.Unauthorized => new UnauthorizedObjectResult(result),
                ResultStatus.Forbidden => new ObjectResult(result) { StatusCode = StatusCodes.Status403Forbidden },
                ResultStatus.Error => new ObjectResult(result) { StatusCode = StatusCodes.Status400BadRequest}
            };
        }
    }
}
