using Sanchime.EventFlows.Events;
using Sanchime.EventFlows.Seedworks;

namespace Sanchime.EventFlows.Entities;

public abstract class Entity : IEntity<long>
{
    public virtual long Id { get; set; } = default!;

    private int? _requestedHashCode;

    private List<IDomainEvent>? _domainEvents;

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly()!;

    public virtual void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents ??= [];

        _domainEvents.Add(domainEvent);
    }

    public virtual void RemoveDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents?.Remove(domainEvent);
    }

    public virtual void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }

    public bool IsTransient() => Id is 0;

    public override bool Equals(object? obj)
    {
        if (obj == null || obj is not Entity entity)
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (this.GetType() != obj.GetType())
            return false;

        if (entity.IsTransient() || this.IsTransient())
            return false;
        else
            return entity.Id == Id;
    }

    public override int GetHashCode()
    {
        if (!IsTransient())
        {
            if (!_requestedHashCode.HasValue)
                _requestedHashCode = Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

            return _requestedHashCode.Value;
        }
        else
        {
            return base.GetHashCode();
        }
    }

    public static bool operator ==(Entity left, Entity right)
    {
        if (Equals(left, null))
        {
            return Equals(right, null);
        }
        else
        {
            return left.Equals(right);
        }
    }

    public static bool operator !=(Entity left, Entity right)
    {
        return !(left == right);
    }
}
