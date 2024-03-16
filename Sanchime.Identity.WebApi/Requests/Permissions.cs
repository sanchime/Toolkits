namespace Sanchime.Identity.WebApi.Requests;

public record PermissionByIdRequest(long PermissionId);

public record PermissionRequest(string Code, string Name, string? Description);