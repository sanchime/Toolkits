namespace Sanchime.Identity.Queries;

public record GetRoleListQuery : IQuery<List<RoleResponse>>;

public record GetRoleMenusQuery(long Id) : IQuery<List<MenuTreeResponse>>;