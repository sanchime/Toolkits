namespace Sanchime.Identity.Handlers;

public class RoleCommandHandler(IdentityContext context) :
    ICommandHandler<AddRoleCommand>,
    ICommandHandler<UpdateRoleCommand>,
    ICommandHandler<DeleteRoleCommand>
{
    public async Task Handle(AddRoleCommand command, CancellationToken cancellation = default)
    {
        if (await context.Roles.AnyAsync(x => command.RoleCode == x.Code, cancellationToken: cancellation))
        {
            throw new Exception($"已经存在编号为{command.RoleCode}的角色了");
        }

        var role = new Role { Code = command.RoleCode, Name = command.RoleName, Description = command.Description };
        await context.Roles.AddAsync(role, cancellation);
        await context.SaveChangesAsync(cancellation);
    }

    public async Task Handle(UpdateRoleCommand command, CancellationToken cancellation = default)
    {
        var role = await context.Roles
            .FirstOrDefaultAsync(x => x.Id == command.RoleId, cancellationToken: cancellation)
            ?? throw new Exception("该角色不存在");

        role.Name = command.RoleName;
        role.Code = command.RoleCode;
        role.Description = command.Description;

        await context.SaveChangesAsync(cancellation);
    }

    public async Task Handle(DeleteRoleCommand command, CancellationToken cancellation = default)
    {
        var role = await context.Roles
            .FirstOrDefaultAsync(x => x.Id == command.RoleId, cancellationToken: cancellation)
            ?? throw new Exception("该角色不存在");

        context.Roles.Remove(role);

        await context.SaveChangesAsync(cancellation);
    }
}
