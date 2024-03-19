namespace Sanchime.Identity.Commands;

public record AddPermissionCommand(string Code, string Name, string? Description) : ICommand;

public record class UpdatePermissionCommand(long Id, string Code, string Name, string? Description, bool IsEnabled) : ICommand;
