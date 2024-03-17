namespace Sanchime.Identity.Handlers;

public class RoleCommandHandler(IdentityContext context) :
    ICommandHandler<AddRoleCommand>,
    ICommandHandler<UpdateRoleCommand>,
    ICommandHandler<DeleteRoleCommand>,
    ICommandHandler<UpdateRolePermissionCommand>
{
    public async Task Handle(AddRoleCommand command, CancellationToken cancellation = default)
    {
        if (await context.Roles.AnyAsync(x => command.Code == x.Code, cancellationToken: cancellation))
        {
            throw new Exception($"已经存在编号为{command.Code}的角色了");
        }

        var role = new Role { Code = command.Code, Name = command.Name, Description = command.Description };
        await context.Roles.AddAsync(role, cancellation);
        await context.SaveChangesAsync(cancellation);
    }

    public async Task Handle(UpdateRoleCommand command, CancellationToken cancellation = default)
    {
        var role = await context.Roles
            .FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken: cancellation)
            ?? throw new Exception("该角色不存在");

        role.Name = command.Name;
        role.Code = command.Code;
        role.Description = command.Description;

        await context.SaveChangesAsync(cancellation);
    }

    public async Task Handle(DeleteRoleCommand command, CancellationToken cancellation = default)
    {
        var role = await context.Roles
            .FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken: cancellation)
            ?? throw new Exception("该角色不存在");

        context.Roles.Remove(role);

        await context.SaveChangesAsync(cancellation);
    }

    public async Task Handle(UpdateRolePermissionCommand command, CancellationToken cancellation = default)
    {
        var role = await context.Roles
            .Include(x => x.Permissions)
            .FirstOrDefaultAsync(x => x.Id == command.RoleId, cancellationToken: cancellation)
            ?? throw new Exception("该角色不存在");

        var permissions = await context.Permissions
            .Where(x => command.Permissions.Contains(x.Id))
            .ToListAsync(cancellationToken: cancellation);

        role.Permissions.Clear();
        role.Permissions = permissions;

        await context.SaveChangesAsync(cancellation);
    }
}
