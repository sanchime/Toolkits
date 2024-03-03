namespace Sanchime.Identity.WebApi.Requests;

public record RoleByIdRequest(long RoleId);

public record RoleRequest(string RoleCode, string RoleName, string? Description);