using Sanchime.AspNetCore;
using Sanchime.Identity.Commands;
using Sanchime.Identity.Queries;
using Sanchime.Identity.Responses;
namespace Sanchime.ProjectManager.WebApi.ApiGroups;

public static class IdentityGroup
{
    public static IEndpointRouteBuilder AddIdentityGroup(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGroup("identity")
            .AddAccountApi()
            .AddUserApi()
            .AddRoleApi()
            ;

        return endpoint;
    }

    private static IEndpointRouteBuilder AddAccountApi(this IEndpointRouteBuilder endpoint)
    {
        var api = endpoint.MapGroup("account").WithTags("帐号管理");
        api.MapPost<LoginCommand, LoginResponse>("token", x => x.AllowAnonymous());
        api.MapPost<RefreshTokenCommand, LoginResponse>("token/refresh");
        api.MapPut<ResetPasswordCommand>("password/reset");
        api.MapPut<BatchResetPasswordCommand>("password/reset/batch");
        api.MapPut<ChangePasswordCommand>("password/change");

        return endpoint;
    }

    private static IEndpointRouteBuilder AddUserApi(this IEndpointRouteBuilder endpoint)
    {
        var api = endpoint.MapGroup("user").WithTags("用户管理")
            .MapGet<GetUserByIdQuery, UserResponse>("{UserId}")
            .MapPost<AddUserCommand>()
            .MapPut<UpdateUserCommand>()
            .MapDelete<DeleteUserCommand>("{UserId}");

        api.MapGroup("{UserId}/roles")
            .MapPost<AddUserRolesCommand>()
            .MapGet<GetUserRolesQuery, UserRolesResponse>()
            .MapDelete<DeleteUserRolesCommand>();

        return endpoint;
    }
    private static IEndpointRouteBuilder AddRoleApi(this IEndpointRouteBuilder endpoint)
    {
        var api = endpoint.MapGroup("role").WithTags("角色管理")
            .MapGet<GetRoleListQuery, List<RoleResponse>>()
            .MapPut<UpdateRoleCommand>()
            .MapPost<AddRoleCommand>()
            .MapDelete<DeleteRoleCommand>("{RoleId}");
        return endpoint;
    }
}
