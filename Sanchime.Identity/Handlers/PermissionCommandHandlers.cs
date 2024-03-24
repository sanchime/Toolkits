namespace Sanchime.Identity.Handlers;

internal class PermissionCommandHandlers(IdentityContext context) :
    ICommandHandler<AddPermissionCommand>,
    ICommandHandler<UpdatePermissionCommand>,
    ICommandHandler<DeletePermissionCommand>
{
    public async Task Handle(AddPermissionCommand command, CancellationToken cancellation = default)
    {
        await context.Permissions.AddAsync(new Permission
        {
            Code = command.Code,
            Name = command.Name,
            Description = command.Description
        }, cancellation);

        await context.SaveChangesAsync(cancellation);
    }

    public async Task Handle(UpdatePermissionCommand command, CancellationToken cancellation = default)
    {
        var permission = await context.Permissions.FindAsync([command.Id], cancellationToken: cancellation)
            ?? throw new NullReferenceException("权限不存在");
        permission.Code = command.Code;
        permission.Name = command.Name;
        permission.Description = command.Description;
        await context.SaveChangesAsync(cancellation);
    }

    public async Task Handle(DeletePermissionCommand command, CancellationToken cancellation = default)
    {
        var permission = await context.Permissions.FindAsync([command.Id], cancellationToken: cancellation)
            ?? throw new NullReferenceException("权限不存在");

        context.Permissions.Remove(permission);

        await context.SaveChangesAsync(cancellation);
    }
}
