using Sanchime.Identity.Enums;

namespace Sanchime.Identity.Commands;

public class AddMenuCommand : ICommand
{

    public long? ParentId { get; set; }

    public required string Code { get; set; }

    public required string Name { get; set; }

    public MenuType Type { get; set; }

    public string? Icon { get; set; }

    public int Order { get; set; }

    public string? Route { get; set; }

    public string? Path { get; set; }

    public string? Description { get; set; }
}