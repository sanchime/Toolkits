namespace Sanchime.Identity.Commands;

public record AddUserCommand(string UserName) : ICommand;

public record UpdateUserCommand(long Id, string UserName, bool IsEnabled) : ICommand;

public record DeleteUserCommand(long Id) : ICommand;

public record AddUserRolesCommand(long Id, long[] Roles) : ICommand;

public record DeleteUserRolesCommand(long Id) : ICommand;