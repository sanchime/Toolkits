
using Sanchime.EntityFrameworkCore;

namespace Sanchime.Identity.Handlers;

public class PermissionQueryHandlers(IdentityContext context, IMapper mapper) :
    IQueryHandler<GetPermissionListQuery, PaginatedResult<PermissionResponse>>
{
    public async Task<PaginatedResult<PermissionResponse>> Handle(GetPermissionListQuery query, CancellationToken cancellation = default)
    {
        return await context.Permissions
            .ProjectTo<PermissionResponse>(mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToPageListAsync(query);
    }
}
