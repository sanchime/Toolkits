using Sanchime.EventFlows.Entities;

namespace Sanchime.Identity.Entities;

public class RoleMenu : Entity
{
    public long RoleId { get; set; }

    public required Role Role { get; set; }

    public long MenuId { get; set; }

    public required Menu Menu { get; set; }

    public IList<Permission> Permissions { get; set; } = [];
}
