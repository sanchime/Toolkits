namespace Sanchime.ProjectManager.WebApi.ApiGroups;

public static class IdentityGroup
{
    public static IEndpointRouteBuilder AddIdentityGroup(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGroup("identity")
            .AddUserApi()
            .AddAccountApi()
            .AddRoleApi()
            .AddPermissionApi()
            .AddMenuApi()
            ;

        return endpoint;
    }

    private static IEndpointRouteBuilder AddAccountApi(this IEndpointRouteBuilder endpoint)
    {
        var api = endpoint.MapGroup("account").WithTags("帐号管理");
        api.MapPost<RegisterAccountCommand>("register");
        api.MapPost<LoginCommand, LoginResponse>("token", x => x.AllowAnonymous());
        api.MapPost<RefreshTokenCommand, LoginResponse>("token/refresh");
        api.MapPatch<RequestById, ResetPasswordCommand>("{Id}/password/reset", (p) => new ResetPasswordCommand(p.Id));
        api.MapPost<BatchResetPasswordCommand>("password/reset/batch");
        api.MapPatch<RequestById, AccountChangePasswordRequest, ChangePasswordCommand>("{Id}/password/change", (p, b) => new ChangePasswordCommand(p.Id, b.Password, b.NewPassword));

        return endpoint;
    }

    private static IEndpointRouteBuilder AddUserApi(this IEndpointRouteBuilder endpoint)
    {
        var api = endpoint.MapGroup("user").WithTags("用户管理")
            .MapGet<GetUserByIdQuery, UserResponse>("{Id}")
            .MapGet<GetUserListQuery, PaginatedResult<UserResponse>>()
            .MapPost<AddUserCommand>()
            .MapPut<RequestById, UserUpdateRequest, UpdateUserCommand>("{Id}", (p, b) => new UpdateUserCommand(p.Id, b.UserName, b.IsEnabled))
            .MapDelete<DeleteUserCommand>("{Id}");

        api.MapGroup("{Id}/roles")
            .MapPost<RequestById, long[], AddUserRolesCommand>("", (p, b) => new(p.Id, b))
            .MapGet<GetUserRolesQuery, UserRolesResponse>()
            .MapDelete<DeleteUserRolesCommand>();

        api.MapGroup("{Id}/menus")
            .MapGet<GetUserMenusQuery, List<MenuTreeResponse>>();

        return endpoint;
    }
    private static IEndpointRouteBuilder AddRoleApi(this IEndpointRouteBuilder endpoint)
    {
        var api = endpoint.MapGroup("role").WithTags("角色管理")
            .MapGet<GetRoleListQuery, List<RoleResponse>>()
            .MapPut<RequestById, RoleRequest, UpdateRoleCommand>("{Id}", (p, b) => new (p.Id, b.Code, b.Name, b.Description, b.IsEnabled))
            .MapPost<AddRoleCommand>()
            .MapDelete<DeleteRoleCommand>("{Id}")
            .MapPost<RequestById, long[], UpdateRolePermissionCommand>("{Id}/permissions", (p, b) => new (p.Id, b));

        api.MapGroup("{Id}/menus")
            .MapGet<GetRoleMenusQuery, List<MenuTreeResponse>>();
        
        return endpoint;
    }

    private static IEndpointRouteBuilder AddPermissionApi(this IEndpointRouteBuilder endpoint)
    {
        var api = endpoint.MapGroup("permission").WithTags("权限管理");
        api.MapGet<GetPermissionListQuery, PaginatedResult<PermissionResponse>>();
        api.MapPost<AddPermissionCommand>();
        api.MapPut<RequestById, PermissionRequest, UpdatePermissionCommand>("{Id}", (p, b) => new (p.Id, b.Code, b.Name, b.Description, b.IsEnabled));
        api.MapDelete<DeletePermissionCommand>();

        api.MapGet<GetPermissionTreeQuery, List<PermissionTreeResponse>>("tree");

        return endpoint;
    }

    private static IEndpointRouteBuilder AddMenuApi(this IEndpointRouteBuilder endpoint)
    {
        var api = endpoint.MapGroup("menu").WithTags("菜单管理");
        api.MapPost<AddMenuCommand>();
        api.MapGet<GetMenuTreeQuery, List<MenuTreeResponse>>("tree");

        return endpoint;
    }
}
