using System.Reflection;

namespace Sanchime.EventFlows.Seedworks;

/// <summary>
/// 枚举类
/// </summary>
public abstract class Enumeration : IComparable<Enumeration>
{
    public string Name { get; }

    public int Id { get; }

    protected Enumeration(int id, string name) => (Id, Name) = (id, name);

    public override string ToString() => Name;

    private static IEnumerable<Enumeration>? _enumerations;

    public static IEnumerable<T> GetAll<T>() where T : Enumeration
    {
        _enumerations ??= typeof(T).GetFields(BindingFlags.Public |
                            BindingFlags.Static |
                            BindingFlags.DeclaredOnly)
                 .Select(f => (T)f.GetValue(null)!);

        return _enumerations.Cast<T>();
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Enumeration otherValue)
        {
            return false;
        }

        var typeMatches = GetType().Equals(obj.GetType());
        var valueMatches = Id.Equals(otherValue.Id);

        return typeMatches && valueMatches;
    }

    public override int GetHashCode() => Id ^ Name.GetHashCode();

    public int CompareTo(Enumeration? other) => other is not null ? Id.CompareTo(other.Id) : 0;

    public static bool operator ==(Enumeration left, Enumeration right) => left is null ? right is null : left.Equals(right);

    public static bool operator !=(Enumeration left, Enumeration right) => !(left == right);

    public static bool operator <(Enumeration left, Enumeration right) => left is null ? right is not null : left.CompareTo(right) < 0;

    public static bool operator <=(Enumeration left, Enumeration right) => left is null || left.CompareTo(right) <= 0;

    public static bool operator >(Enumeration left, Enumeration right) => left?.CompareTo(right) > 0;

    public static bool operator >=(Enumeration left, Enumeration right) => left is null ? right is null : left.CompareTo(right) >= 0;

    // Other utility methods ...
}