namespace Sanchime.Identity.Handlers;

public class UserCommandHandler(IdentityContext context) :
    ICommandHandler<AddUserRolesCommand>,
    ICommandHandler<DeleteUserRolesCommand>
{
    public async Task Handle(DeleteUserRolesCommand command, CancellationToken cancellation = default)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken: cancellation)
            ?? throw new Exception("该用户不存在");
        user.Roles.Clear();
        await context.SaveChangesAsync(cancellation);
    }

    public async Task Handle(AddUserRolesCommand command, CancellationToken cancellation = default)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken: cancellation)
            ?? throw new Exception("该用户不存在");

        var roles = await context.Roles.Where(x => command.Roles.Contains(x.Id)).ToListAsync(cancellationToken: cancellation);

        user.Roles.Clear();

        foreach (var role in roles)
        {
            user.Roles.Add(role);
        }

        await context.SaveChangesAsync(cancellation);
    }
}
