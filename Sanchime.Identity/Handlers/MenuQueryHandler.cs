namespace Sanchime.Identity.Handlers;

public class MenuQueryHandler(IdentityContext context) : 
    IQueryHandler<GetMenuTreeQuery, List<MenuTreeResponse>>
{
    public async Task<List<MenuTreeResponse>> Handle(GetMenuTreeQuery query, CancellationToken cancellation = default)
    {
        return await context.Menus
            .ProjectToType<MenuTreeResponse>()
            .Where(x => x.Type == query.Type)
            .OrderBy(x => x.Order)
            .AsNoTracking()
            .ToTreeAsync(null, x => x.Id, x => x.ParentId, cancellation);
    }
}