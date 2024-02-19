namespace Domain.Common.Primitives;

public abstract class ValueObject
    : IEquatable<ValueObject>
{
    /// <inheritdoc />
    public bool Equals(
        ValueObject? other)
    {
        return other switch
        {
            null => false,
            _ => GetEqualityComponents().SequenceEqual(other.GetEqualityComponents())
        };
    }

    /// <inheritdoc />
    public override bool Equals(
        object? obj)
    {
        return obj switch
        {
            null => false,
            ValueObject valueObject => Equals(valueObject),
            _ => false
        };
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        HashCode hashCode = default;

        foreach (var value in GetEqualityComponents())
        {
            hashCode.Add(value);
        }

        return hashCode.ToHashCode();
    }

    public static bool operator ==(
        ValueObject? left,
        ValueObject? right)
    {
        if (left is null && right is null)
        {
            return true;
        }

        if (left is null || right is null)
        {
            return false;
        }

        return left.Equals(right);
    }

    public static bool operator !=(
        ValueObject? left,
        ValueObject? right)
    {
        return !(left == right);
    }

    protected abstract IEnumerable<object> GetEqualityComponents();
}
