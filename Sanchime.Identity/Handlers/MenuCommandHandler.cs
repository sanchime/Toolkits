namespace Sanchime.Identity.Handlers;

internal class MenuCommandHandler(IdentityContext context) :
    ICommandHandler<AddMenuCommand>
{
    public async Task Handle(AddMenuCommand command, CancellationToken cancellation = default)
    {
        var menu = command.Adapt<Menu>();
        
        await context.AddAsync(menu, cancellation);

        await context.SaveChangesAsync(cancellation);
    }
}
