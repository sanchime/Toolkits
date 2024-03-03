namespace Sanchime.Identity.Handlers;

public class RoleQueryHandler(IdentityContext context) : IQueryHandler<GetRoleListQuery, List<RoleResponse>>
{
    public Task<List<RoleResponse>> Handle(GetRoleListQuery query, CancellationToken cancellation = default)
    {
        return context.Roles
            .ProjectToType<RoleResponse>()
            .AsNoTracking()
            .ToListAsync(cancellationToken: cancellation);
    }
}
