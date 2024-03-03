

namespace Sanchime.Identity.Handlers;

public class UserQueryHandler(IdentityContext context) :
    IQueryHandler<GetUserByIdQuery, UserResponse>,
    IQueryHandler<GetUserRolesQuery, UserRolesResponse>
{
    public async Task<UserResponse> Handle(GetUserByIdQuery query, CancellationToken cancellation = default)
    {
        return await context.Users
            .ProjectToType<UserResponse>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == query.UserId, cancellationToken: cancellation)
            ?? throw new NullReferenceException("找不到此用户");
    }

    public async Task<UserRolesResponse> Handle(GetUserRolesQuery query, CancellationToken cancellation = default)
    {
        return await context.Users
            .Include(x => x.Roles)
            .ProjectToType<UserRolesResponse>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == query.UserId, cancellationToken: cancellation)
            ?? throw new NullReferenceException("找不到此用户");
    }
}
