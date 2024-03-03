using Microsoft.AspNetCore.Http;
using Sanchime.Common;
using Sanchime.Common.Models;
using Sanchime.DependencyInjection;

namespace Sanchime.AspNetCore;

[ScopedInjection(Interface = typeof(ICurrentUserContext))]
internal sealed class CurrentUserContext : ICurrentUserContext
{
    public CurrentUserContext(IHttpContextAccessor accessor)
    {
        Account = accessor.HttpContext?.User.Identities.FirstOrDefault(x => x.NameClaimType == nameof(ICurrentUserContext.Account))?.Name ?? "System";
        UserId = accessor.HttpContext?.User.Identities.FirstOrDefault(x => x.NameClaimType == nameof(ICurrentUserContext.UserId))?.Name.Parse<long>() ?? default;
        UserName = accessor.HttpContext?.User.Identities.FirstOrDefault(x => x.NameClaimType == nameof(ICurrentUserContext.UserName))?.Name ?? "System";
        Token = accessor.HttpContext?.Request.Headers.Authorization!;
        IsLogined = Token is null;
    }

    public bool IsLogined { get; }
    public string Account { get; }
    public string UserName { get; }
    public string? Token { get; }
    public long UserId { get; set; }
}
