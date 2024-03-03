using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Sanchime.Identity.Handlers;

public class AccountCommandHandler(IdentityContext context, IOptions<IdentityConfiguration> config) :
    ICommandHandler<LoginCommand, LoginResponse>,
    ICommandHandler<RefreshTokenCommand, LoginResponse>,
    ICommandHandler<RegisterAccountCommand>,
    ICommandHandler<ChangePasswordCommand>,
    ICommandHandler<ResetPasswordCommand>,
    ICommandHandler<BatchResetPasswordCommand>
{
    /// <summary>
    /// 登录获取Token
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    /// <exception cref="Exception"></exception>
    public async Task<LoginResponse> Handle(LoginCommand command, CancellationToken cancellation = default)
    {
        var account = await context.Accounts
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.LoginName == command.Account, cancellationToken: cancellation)
            ?? throw new NullReferenceException("帐号不存在");
        if (!PasswordHelper.VerifyHashedPassword(command.Password, account.PasswordHash))
        {
            throw new Exception("密码错误");
        }

        account.RefreshToken = JwtHelper.GenerateRefreshToken();
        account.RefreshTokenExpiryTime = DateTimeOffset.UtcNow.Add(config.Value.Expiration);

        string token = JwtHelper.GenerateJwtToken(account, config.Value);

        await context.SaveChangesAsync(cancellation);

        return new LoginResponse(token, account.RefreshTokenExpiryTime.Value, account.RefreshToken);
    }

    /// <summary>
    /// 刷新Token
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<LoginResponse> Handle(RefreshTokenCommand command, CancellationToken cancellation = default)
    {
        var userPrincipal = JwtHelper.GetPrincipalFromExpiredToken(config.Value, command.Token, "无效的Token");

        var userId = userPrincipal.FindFirstValue(ClaimTypes.NameIdentifier).Parse<long>();

        var account = await context.Accounts.FirstOrDefaultAsync(x => x.User.Id == userId, cancellationToken: cancellation) ?? throw new Exception("帐号不存在");

        if (account.RefreshToken != command.RefreshToken || account.RefreshTokenExpiryTime <= DateTimeOffset.UtcNow)
            throw new Exception("客户端Token无效");

        account.RefreshToken = JwtHelper.GenerateRefreshToken();

        string token = JwtHelper.GenerateJwtToken(account, config.Value);

        await context.SaveChangesAsync(cancellation);

        return new LoginResponse(token, account.RefreshTokenExpiryTime!.Value, account.RefreshToken);
    }

    /// <summary>
    /// 注册账户
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    public async Task Handle(RegisterAccountCommand command, CancellationToken cancellation = default)
    {
        var account = new Account
        {
            LoginName = command.Account,
            PasswordHash = PasswordHelper.HashPassword(command.Password),
        };

        await context.Accounts.AddAsync(account, cancellation);
        await context.SaveChangesAsync(cancellation);
    }

    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task Handle(ChangePasswordCommand command, CancellationToken cancellation = default)
    {
        var account = await context.Accounts
            .FirstOrDefaultAsync(x => x.Id == command.AccountId, cancellationToken: cancellation)
            ?? throw new NullReferenceException("账号不存在");

        if (!PasswordHelper.VerifyHashedPassword(command.Password, account.PasswordHash))
        {
            throw new Exception("旧密码错误");
        }

        account.PasswordHash = PasswordHelper.HashPassword(command.NewPassword);

        await context.SaveChangesAsync(cancellation);
    }

    /// <summary>
    /// 重置默认密码
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task Handle(ResetPasswordCommand command, CancellationToken cancellation = default)
    {
        var account = await context.Accounts
            .FirstOrDefaultAsync(x => x.Id == command.AccountId, cancellationToken: cancellation)
            ?? throw new NullReferenceException("账号不存在");

        account.PasswordHash = PasswordHelper.HashPassword(config.Value.DefaultPassword);

        await context.SaveChangesAsync(cancellation);
    }

    /// <summary>
    /// 批量重置密码
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    public async Task Handle(BatchResetPasswordCommand command, CancellationToken cancellation = default)
    {
        var accounts = await context.Accounts
            .Where(x => command.Accounts.Contains(x.Id))
            .ToListAsync(cancellationToken: cancellation);

        if (accounts.Count > 0)
        {
            foreach (var account in accounts)
            {
                account.PasswordHash = PasswordHelper.HashPassword(config.Value.DefaultPassword);
            }

            await context.SaveChangesAsync(cancellation);
        }
    }
}
