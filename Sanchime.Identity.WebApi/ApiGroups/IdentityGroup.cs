using Sanchime.AspNetCore;
using Sanchime.Identity.Commands;
using Sanchime.Identity.Queries;
using Sanchime.Identity.Responses;
using Sanchime.Identity.WebApi.Requests;
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
        api.MapPatch<AccountByIdRequest, ResetPasswordCommand>("{AccountId}/password/reset", (p) => new ResetPasswordCommand(p.AccountId));
        api.MapPost<BatchResetPasswordCommand>("password/reset/batch");
        api.MapPatch<AccountByIdRequest, AccountChangePasswordRequest, ChangePasswordCommand>("{AccountId}/password/change", (p, b) => new ChangePasswordCommand(p.AccountId, b.Password, b.NewPassword));

        return endpoint;
    }

    private static IEndpointRouteBuilder AddUserApi(this IEndpointRouteBuilder endpoint)
    {
        var api = endpoint.MapGroup("user").WithTags("用户管理")
            .MapGet<GetUserByIdQuery, UserResponse>("{UserId}")
            .MapPost<AddUserCommand>()
            .MapPut<UserByIdRequest, UserUpdateRequest, UpdateUserCommand>("{UserId}", (p, b) => new UpdateUserCommand(p.UserId, b.UserName))
            .MapDelete<DeleteUserCommand>("{UserId}");

        api.MapGroup("{UserId}/roles")
            .MapPost<UserRoleByUserIdRequest, long[], AddUserRolesCommand>("", (p, b) => new(p.UserId, b))
            .MapGet<GetUserRolesQuery, UserRolesResponse>()
            .MapDelete<DeleteUserRolesCommand>();

        return endpoint;
    }
    private static IEndpointRouteBuilder AddRoleApi(this IEndpointRouteBuilder endpoint)
    {
        var api = endpoint.MapGroup("role").WithTags("角色管理")
            .MapGet<GetRoleListQuery, List<RoleResponse>>()
            .MapPut<RoleByIdRequest, RoleRequest, UpdateRoleCommand>("{RoleId}", (p, b) => new (p.RoleId, b.RoleCode, b.RoleName, b.Description))
            .MapPost<AddRoleCommand>()
            .MapDelete<DeleteRoleCommand>("{RoleId}");
        return endpoint;
    }
}
