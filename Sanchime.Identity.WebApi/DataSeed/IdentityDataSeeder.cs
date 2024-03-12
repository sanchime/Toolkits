using Microsoft.Extensions.Options;
using Sanchime.DependencyInjection;
using Sanchime.EntityFrameworkCore;

namespace Sanchime.Identity.WebApi;

[ScopedInjection]
internal class IdentityDataSeeder(IEventFlowMediator mediator, IdentityContext context, ILogger<IdentityDataSeeder> logger, IOptions<IdentityConfiguration> config) : IDataSeeder
{
    public async ValueTask Initialize()
    {
        if (!await context.Accounts.AnyAsync(x  => x.LoginName == config.Value.DefaultAdministratorAccount))
        {
            logger.LogInformation("正在初始化账号：{account}", config.Value.DefaultAdministratorAccount);
            await mediator.Execute(new RegisterAccountCommand(config.Value.DefaultAdministratorAccount, config.Value.DefaultAdministratorPassword));
        }
        if (!await context.Roles.AnyAsync(x => x.Code == config.Value.DefaultRoleCode))
        {
            await mediator.Execute(new AddRoleCommand(config.Value.DefaultRoleCode, config.Value.DefaultRoleName, config.Value.DefaultRoleName));
        }
    }
}
