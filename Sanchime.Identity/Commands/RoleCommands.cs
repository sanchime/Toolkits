namespace Sanchime.Identity.Commands;

public record AddRoleCommand(string Code, string Name, string? Description) : ICommand;

public record UpdateRoleCommand(long Id, string Code, string Name, string? Description) : ICommand;

public record DeleteRoleCommand(long Id) : ICommand;
