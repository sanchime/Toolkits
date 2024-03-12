using AutoMapper.QueryableExtensions;

namespace Sanchime.Identity.Handlers;

public class RoleQueryHandler(IdentityContext context, IMapper mapper) : IQueryHandler<GetRoleListQuery, List<RoleResponse>>
{
    public Task<List<RoleResponse>> Handle(GetRoleListQuery query, CancellationToken cancellation = default)
    {
        return context.Roles
            .ProjectTo<RoleResponse>(mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync(cancellationToken: cancellation);
    }
}
