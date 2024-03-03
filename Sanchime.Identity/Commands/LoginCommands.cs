using Sanchime.Identity.Responses;

namespace Sanchime.Identity.Commands;

public record LoginCommand(string Account, string Password) : ICommand<LoginResponse>;

public record RefreshTokenCommand(string Token, string RefreshToken) : ICommand<LoginResponse>;
