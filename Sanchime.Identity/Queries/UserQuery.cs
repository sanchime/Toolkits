namespace Sanchime.Identity.Queries;

public record GetUserListQuery(int PageIndex = 1, int PageSize = 20) : IPageableObject, IQuery<PaginatedResult<UserResponse>>;

public record GetUserByIdQuery(long Id) : IQuery<UserResponse>;


public record GetUserRolesQuery(long Id) : IQuery<UserRolesResponse>;