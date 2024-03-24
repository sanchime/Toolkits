using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;

namespace Sanchime.AspNetCore;

internal class ExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        httpContext.Response.ContentType = MediaTypeNames.Application.Json;

        await httpContext.Response.WriteAsJsonAsync(new ErrorResult
        {
            Code = httpContext.Response.StatusCode,
            Message = exception.Message
        }, cancellationToken);

        return true;
    }
}
