﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Sanchime.EventFlows;
using System.Diagnostics.CodeAnalysis;

namespace Sanchime.AspNetCore;

public static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapGet<TRequest, TResponse>(this IEndpointRouteBuilder endpoint, [StringSyntax("Route")] string pattern = "", Action<IEndpointConventionBuilder>? config = null) where TRequest : IQuery<TResponse>
    {
        var api = endpoint.MapGet(pattern, ([AsParameters] TRequest request, [FromServices] IEventFlowMediator mediator) => mediator.Request<TRequest, TResponse>(request));
        config?.Invoke(api);
        return endpoint;
    }

    #region Http Patch

    public static IEndpointRouteBuilder MapPatch<TParameter, TRequest>(this IEndpointRouteBuilder endpoint,
       [StringSyntax("Route")] string pattern,
       Func<TParameter, TRequest> injector,
       Action<IEndpointConventionBuilder>? config = null)
       where TRequest : ICommand
    {
        ArgumentNullException.ThrowIfNull(nameof(injector));
        var api = endpoint.MapPatch(pattern, ([AsParameters] TParameter parameter, [FromServices] IEventFlowMediator mediator) => mediator.Execute(injector(parameter)));
        config?.Invoke(api);
        return endpoint;
    }

    public static IEndpointRouteBuilder MapPatch<TParameter, TBody, TRequest>(this IEndpointRouteBuilder endpoint,
       [StringSyntax("Route")] string pattern,
       Func<TParameter, TBody, TRequest> injector,
       Action<IEndpointConventionBuilder>? config = null)
       where TRequest : ICommand
    {
        ArgumentNullException.ThrowIfNull(nameof(injector));
        var api = endpoint.MapPatch(pattern, ([AsParameters] TParameter parameter, [FromBody] TBody body, [FromServices] IEventFlowMediator mediator) => mediator.Execute(injector(parameter, body)));
        config?.Invoke(api);
        return endpoint;
    }

    public static IEndpointRouteBuilder MapPatch<TRequest>(this IEndpointRouteBuilder endpoint, [StringSyntax("Route")] string pattern = "", Action<IEndpointConventionBuilder>? config = null) where TRequest : ICommand
    {
        var api = endpoint.MapPatch(pattern, (TRequest request, [FromServices] IEventFlowMediator mediator) => mediator.Execute(request));
        config?.Invoke(api);
        return endpoint;
    }

    public static IEndpointRouteBuilder MapPatch<TRequest, TResponse>(this IEndpointRouteBuilder endpoint, [StringSyntax("Route")] string pattern = "", Action<IEndpointConventionBuilder>? config = null) where TRequest : ICommand<TResponse>
    {
        var api = endpoint.MapPatch(pattern, (TRequest request, [FromServices] IEventFlowMediator mediator) => mediator.Execute<TRequest, TResponse>(request));
        config?.Invoke(api);
        return endpoint;
    }

    #endregion

    #region Http Put
    public static IEndpointRouteBuilder MapPut<TParameter, TBody, TRequest>(this IEndpointRouteBuilder endpoint, 
        [StringSyntax("Route")] string pattern,
        Func<TParameter, TBody, TRequest> injector,
        Action<IEndpointConventionBuilder>? config = null) 
        where TRequest : ICommand
    {
        ArgumentNullException.ThrowIfNull(nameof(injector));
        var api = endpoint.MapPut(pattern, ([AsParameters] TParameter parameter, [FromBody]TBody body, [FromServices] IEventFlowMediator mediator) => mediator.Execute(injector(parameter, body)));
        config?.Invoke(api);
        return endpoint;
    }

    public static IEndpointRouteBuilder MapPut<TRequest>(this IEndpointRouteBuilder endpoint, [StringSyntax("Route")] string pattern = "", Action<IEndpointConventionBuilder>? config = null) where TRequest : ICommand
    {
        var api = endpoint.MapPut(pattern, (TRequest request, [FromServices] IEventFlowMediator mediator) => mediator.Execute(request));
        config?.Invoke(api);
        return endpoint;
    }

    public static IEndpointRouteBuilder MapPut<TRequest, TResponse>(this IEndpointRouteBuilder endpoint, [StringSyntax("Route")] string pattern = "", Action<IEndpointConventionBuilder>? config = null) where TRequest : ICommand<TResponse>
    {
        var api = endpoint.MapPut(pattern, (TRequest request, [FromServices] IEventFlowMediator mediator) => mediator.Execute<TRequest, TResponse>(request));
        config?.Invoke(api);
        return endpoint;
    }

    #endregion

    #region Http Post

    public static IEndpointRouteBuilder MapPost<TParameter, TBody, TRequest>(this IEndpointRouteBuilder endpoint,
        [StringSyntax("Route")] string pattern,
        Func<TParameter, TBody, TRequest> injector,
        Action<IEndpointConventionBuilder>? config = null)
        where TRequest : ICommand
    {
        ArgumentNullException.ThrowIfNull(nameof(injector));
        var api = endpoint.MapPost(pattern, ([AsParameters] TParameter parameter, [FromBody] TBody body, [FromServices] IEventFlowMediator mediator) => mediator.Execute(injector(parameter, body)));
        config?.Invoke(api);
        return endpoint;
    }

    public static IEndpointRouteBuilder MapPost<TRequest>(this IEndpointRouteBuilder endpoint, [StringSyntax("Route")] string pattern = "", Action<IEndpointConventionBuilder>? config = null) where TRequest : ICommand
    {
        var api = endpoint.MapPost(pattern, (TRequest request, [FromServices] IEventFlowMediator mediator) => mediator.Execute(request));
        config?.Invoke(api);
        return endpoint;
    }

    public static IEndpointRouteBuilder MapPost<TRequest, TResponse>(this IEndpointRouteBuilder endpoint, [StringSyntax("Route")] string pattern = "", Action<IEndpointConventionBuilder>? config = null)
        where TRequest : ICommand<TResponse>
    {
        var api = endpoint.MapPost(pattern, (TRequest request, [FromServices] IEventFlowMediator mediator) => mediator.Execute<TRequest, TResponse>(request));
        config?.Invoke(api);
        return endpoint;
    }

    #endregion

    #region Http Delete

    public static IEndpointRouteBuilder MapDelete<TRequest>(this IEndpointRouteBuilder endpoint, [StringSyntax("Route")] string pattern = "", Action<IEndpointConventionBuilder>? config = null)
        where TRequest : ICommand
    {
        var api = endpoint.MapDelete(pattern, ([AsParameters] TRequest request, [FromServices] IEventFlowMediator mediator) => mediator.Execute(request));
        config?.Invoke(api);
        return endpoint;
    }

    public static IEndpointRouteBuilder MapDelete<TRequest, TResponse>(this IEndpointRouteBuilder endpoint, [StringSyntax("Route")] string pattern = "", Action<IEndpointConventionBuilder>? config = null)
        where TRequest : ICommand<TResponse>
    {
        var api = endpoint.MapDelete(pattern, ([AsParameters] TRequest request, [FromServices] IEventFlowMediator mediator) => mediator.Execute<TRequest, TResponse>(request));
        config?.Invoke(api);
        return endpoint;
    }

    #endregion
}
