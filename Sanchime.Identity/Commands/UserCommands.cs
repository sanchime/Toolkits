namespace Sanchime.Identity.Commands;

public record AddUserCommand(string UserName) : ICommand;

public record UpdateUserCommand(long UserId, string UserName) : ICommand;

public record DeleteUserCommand(long UserId) : ICommand;

public record AddUserRolesCommand(long UserId, long[] Roles) : ICommand;

public record DeleteUserRolesCommand(long UserId) : ICommand;