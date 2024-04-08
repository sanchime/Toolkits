namespace Sanchime.Identity.Queries;

public record GetMenuTreeQuery(MenuType Type) : IQuery<List<MenuTreeResponse>>;