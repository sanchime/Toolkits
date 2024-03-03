using Microsoft.AspNetCore.Http;

namespace Sanchime.AspNetCore.Filters;

public class AuthorizeFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var response = await next(context);

        return response;
    }
}
