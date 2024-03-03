using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;

namespace Sanchime.AspNetCore;

public static class WebApplicationExtensions
{
    public static WebApplication UsePrimitiveConfigure(this WebApplication app)
    {
        // 启用响应压缩
        app.UseResponseCompression();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        return app;
    }

    public static RouteGroupBuilder UseMiniApi(this WebApplication app, [StringSyntax("Route")] string prefix = "api", params IEndpointFilter[]? filters)
    {
        var api = app.MapGroup(prefix);
        if (filters is { Length : > 0})
        {
            foreach (var filter in filters)
            {
                api.AddEndpointFilter(filter);
            }
        }
        return api;
    }
}
