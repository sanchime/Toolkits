using Sanchime.Identity.Enums;

namespace Sanchime.Identity.Entities;

public class Menu : IdentityEntity
{
    public required string Code { get; set; }

    public required string Name { get; set; }

    public string? Icon { get; set; }

    public int Order { get; set; }

    public string? Route { get; set; }

    public string? Path { get; set; }

    public string? Description { get; set; }

    public long? ParentId { get; set; }

    public Menu? Parent { get; set; }

    public ICollection<Menu> Children { get; } = [];

    public IList<Role> Roles { get; set; } = [];

    public IList<RoleMenu> RoleMenus { get; set; } = [];
}
