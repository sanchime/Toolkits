namespace Sanchime.Identity.Queries;

public record GetUserListQuery(int PageIndex = 1, int PageSize = 20) : IPageableObject, IQuery<PaginatedResult<UserResponse>>;

public record GetUserByIdQuery(long UserId) : IQuery<UserResponse>;


public record GetUserRolesQuery(long UserId) : IQuery<UserRolesResponse>;