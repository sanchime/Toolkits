namespace Sanchime.Identity.Queries;

public record GetUserByIdQuery(long UserId) : IQuery<UserResponse>;


public record GetUserRolesQuery(long UserId) : IQuery<UserRolesResponse>;