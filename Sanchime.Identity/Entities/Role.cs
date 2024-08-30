namespace Sanchime.Identity.Entities;

public class Role : IdentityEntity
{
    public required string Code { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public long? ParentId { get; set; }

    public Role? Parent { get; set; }

    public List<User> Users { get; set; } = [];

    public ICollection<Role> Children { get; } = [];

    public IList<Permission> Permissions { get; set; } = [];

    public IList<Menu> Menus { get; set; } = [];

    public IList<RoleMenu> RoleMenus { get; set; } = [];
}
