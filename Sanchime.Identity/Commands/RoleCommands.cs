namespace Sanchime.Identity.Commands;

public record AddRoleCommand(string RoleCode, string RoleName, string? Description) : ICommand;

public record UpdateRoleCommand(long RoleId, string RoleCode, string RoleName, string? Description) : ICommand;

public record DeleteRoleCommand(long RoleId) : ICommand;
