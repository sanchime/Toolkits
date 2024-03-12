

using Sanchime.EntityFrameworkCore;

namespace Sanchime.Identity.Handlers;

public class UserQueryHandler(IdentityContext context, IMapper mapper) :
    IQueryHandler<GetUserByIdQuery, UserResponse>,
    IQueryHandler<GetUserRolesQuery, UserRolesResponse>,
    IQueryHandler<GetUserListQuery, PaginatedResult<UserResponse>>
{
    public async Task<UserResponse> Handle(GetUserByIdQuery query, CancellationToken cancellation = default)
    {
        return await context.Users
            .Where(x => x.Id == query.UserId)
            .ProjectTo<UserResponse>(mapper.ConfigurationProvider)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken: cancellation)
            ?? throw new NullReferenceException("找不到此用户");
    }

    public async Task<UserRolesResponse> Handle(GetUserRolesQuery query, CancellationToken cancellation = default)
    {
        return await context.Users
            .Include(x => x.Roles)
            .Where(x => x.Id == query.UserId)
            .ProjectTo<UserRolesResponse>(mapper.ConfigurationProvider)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken: cancellation)
            ?? throw new NullReferenceException("找不到此用户");
    }

    public async Task<PaginatedResult<UserResponse>> Handle(GetUserListQuery query, CancellationToken cancellation = default)
    {
        return await context.Users
            .ProjectTo<UserResponse>(mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToPageListAsync(query);
    }
}
