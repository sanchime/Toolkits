namespace Sanchime.EventFlows.Seedworks;

public abstract class ValueObject
{
    public static bool operator ==(ValueObject one, ValueObject two) => EqualOperator(one, two);

    public static bool operator !=(ValueObject one, ValueObject two) => NotEqualOperator(one, two);

    protected static bool EqualOperator(ValueObject left, ValueObject right) => !(left is null ^ right is null) && (ReferenceEquals(left, right) || left!.Equals(right));

    protected static bool NotEqualOperator(ValueObject left, ValueObject right) => !EqualOperator(left, right);

    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj) => obj is ValueObject other && GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());

    public override int GetHashCode() => GetEqualityComponents()
            .Select(x => (x?.GetHashCode()) ?? 0)
            .Aggregate((x, y) => x ^ y);
}