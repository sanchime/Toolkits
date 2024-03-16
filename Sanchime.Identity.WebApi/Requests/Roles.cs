namespace Sanchime.Identity.WebApi.Requests;

public record RoleByIdRequest(long RoleId);

public record RoleRequest(string Code, string Name, string? Description);