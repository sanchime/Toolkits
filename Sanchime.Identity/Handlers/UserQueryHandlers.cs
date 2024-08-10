using Sanchime.EntityFrameworkCore;

namespace Sanchime.Identity.Handlers;

public class UserQueryHandler(IdentityContext context) :
    IQueryHandler<GetUserByIdQuery, UserResponse>,
    IQueryHandler<GetUserRolesQuery, UserRolesResponse>,
    IQueryHandler<GetUserListQuery, PaginatedResult<UserResponse>>,
    IQueryHandler<GetUserMenusQuery, List<MenuTreeResponse>>
{
    public async Task<UserResponse> Handle(GetUserByIdQuery query, CancellationToken cancellation = default)
    {
        return await context.Users
            .Where(x => x.Id == query.Id)
            .ProjectToType<UserResponse>()
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken: cancellation)
            ?? throw new NullReferenceException("找不到此用户");
    }

    public async Task<UserRolesResponse> Handle(GetUserRolesQuery query, CancellationToken cancellation = default)
    {
        return await context.Users
            .Include(x => x.Roles)
            .Where(x => x.Id == query.Id)
            .ProjectToType<UserRolesResponse>()
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken: cancellation)
            ?? throw new NullReferenceException("找不到此用户");
    }

    public async Task<PaginatedResult<UserResponse>> Handle(GetUserListQuery query, CancellationToken cancellation = default)
    {
        return await context.Users
            .ProjectToType<UserResponse>()
            .AsNoTracking()
            .ToPageListAsync(query);
    }

    public async Task<List<MenuTreeResponse>> Handle(GetUserMenusQuery query, CancellationToken cancellation = default)
    {
        var baseQuery = from user in context.Users
                    from role in user.Roles
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
