namespace Sanchime.Identity.WebApi.Requests;
public record struct PermissionRequest(string Code, string Name, string? Description, bool IsEnabled);