namespace Sanchime.Identity.Queries;

public record GetPermissionListQuery(int PageIndex = 1, int PageSize = 20) : IPageableObject, IQuery<PaginatedResult<PermissionResponse>>;