namespace Sanchime.Identity.Handlers;

public class RoleQueryHandler(IdentityContext context) : 
    IQueryHandler<GetRoleListQuery, List<RoleResponse>>,
    IQueryHandler<GetRoleMenusQuery, List<MenuTreeResponse>>
{
    public Task<List<RoleResponse>> Handle(GetRoleListQuery query, CancellationToken cancellation = default)
    {
        return context.Roles
            .ProjectToType<RoleResponse>()
            .AsNoTracking()
            .ToListAsync(cancellationToken: cancellation);
    }

    public async Task<List<MenuTreeResponse>> Handle(GetRoleMenusQuery query, CancellationToken cancellation = default)
    {
        var baseQuery = from role in context.Roles
                        from menu in role.Menus
                        select menu;

        var menus = await baseQuery
            .Distinct()
            .ProjectToType<MenuTreeResponse>()
            .AsNoTracking()
            .ToListAsync(cancellationToken: cancellation);

        return menus.ToTree(null, x => x.Id, x => x.ParentId);
    }
}
