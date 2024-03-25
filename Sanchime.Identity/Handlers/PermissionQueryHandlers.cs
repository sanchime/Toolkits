using Sanchime.Common;


namespace Sanchime.Identity.Handlers;

public class PermissionQueryHandlers(IdentityContext context) :
    IQueryHandler<GetPermissionListQuery, PaginatedResult<PermissionResponse>>,
    IQueryHandler<GetPermissionTreeQuery, List<PermissionTreeResponse>>
{
    public async Task<PaginatedResult<PermissionResponse>> Handle(GetPermissionListQuery query, CancellationToken cancellation = default)
    {
        return await context.Permissions
            .ProjectToType<PermissionResponse>()
            .AsNoTracking()
            .ToPageListAsync(query);
    }

    public async Task<List<PermissionTreeResponse>> Handle(GetPermissionTreeQuery query, CancellationToken cancellation = default)
    {
       return await context.Permissions
            .ProjectToType<PermissionTreeResponse>()
            .AsNoTracking()
            .ToTreeAsync(null, x => x.Id, x => x.ParentId, cancellation);
    }
}
