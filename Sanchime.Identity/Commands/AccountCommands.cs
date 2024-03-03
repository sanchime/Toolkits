namespace Sanchime.Identity.Commands;

public record BatchResetPasswordCommand(long[] Accounts) : ICommand;

public record ResetPasswordCommand(long AccountId) : ICommand;

public record ChangePasswordCommand(long AccountId, string Password, string NewPassword) : ICommand;

public record RegisterAccountCommand(string Account, string Password) : ICommand;