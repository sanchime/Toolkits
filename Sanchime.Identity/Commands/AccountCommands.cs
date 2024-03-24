namespace Sanchime.Identity.Commands;

public record BatchResetPasswordCommand(long[] Accounts) : ICommand;

public record ResetPasswordCommand(long Id) : ICommand;

public record ChangePasswordCommand(long Id, string Password, string NewPassword) : ICommand;

public record RegisterAccountCommand(string Account, string Password) : ICommand;