namespace Sanchime.Identity.WebApi.Requests;
public record struct RoleRequest(string Code, string Name, string? Description, bool IsEnabled);