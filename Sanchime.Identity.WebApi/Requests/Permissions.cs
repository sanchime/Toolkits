namespace Sanchime.Identity.WebApi.Requests;

public record struct PermissionByIdRequest(long PermissionId);

public record struct PermissionRequest(string Code, string Name, string? Description, bool IsEnabled);