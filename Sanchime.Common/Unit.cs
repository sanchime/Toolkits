namespace System;

public readonly struct Unit
{
    public static Unit Value { get; } = new();

    static Unit() { }

    public override int GetHashCode() => 0;
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;

        return Equals(this, obj) || (obj is Unit other && Equals(other));
    }

    public override string ToString() => "()";

    public static bool operator ==(Unit left, Unit right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Unit left, Unit right)
    {
        return !(left == right);
    }
}
