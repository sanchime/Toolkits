namespace Sanchime.Identity.Entities;

public class UserGroup : IdentityEntity
{
    public required string Code { get; set; }

    public required string Name { get; set; }

    public long? ParentId { get; set; }

    public UserGroup? Parent { get; set; }

    public ICollection<UserGroup> Children { get; } = [];

    public IList<User> Users { get; set; } = [];
}