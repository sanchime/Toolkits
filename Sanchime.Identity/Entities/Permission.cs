namespace Sanchime.Identity.Entities;

public class Permission : IdentityEntity
{
    public required string Code { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public long? ParentId { get; set; }

    public Permission? Parent { get; set; }

    public ICollection<Permission> Children { get; } = [];

    public Role Role { get; set; } = null!;
}
