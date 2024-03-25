using Sanchime.EntityFrameworkCore;
using Sanchime.EventFlows.Entities;

namespace Sanchime.Identity.Entities;

public abstract class IdentityEntity : Entity, IDeleteable, ITraceableEntity
{
    public bool IsDeleted { get; set; } = false;

    public bool IsEnabled { get; set; } = true;

    public long CreatedUser { get; set; }

    public string CreatedUserName { get; set; } = null!;

    public long ModifiedUser { get; set; }

    public string ModifiedUserName { get; set; } = null!;

    public DateTimeOffset CreatedDate { get; set; }

    public DateTimeOffset ModifiedDate { get; set; }
}