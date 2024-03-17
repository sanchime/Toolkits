namespace Sanchime.Identity.WebApi.Requests;

public record struct RoleByIdRequest(long RoleId);

public record struct RoleRequest(string Code, string Name, string? Description);