namespace AVS.Contacts.Domain.Common;

public abstract class BaseEntity<TId>
{
    public TId Id { get; protected set; } = default!;
    public DateTimeOffset CreatedAt { get; protected set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; protected set; } = DateTimeOffset.UtcNow;

    protected void UpdateTimestamp() => UpdatedAt = DateTimeOffset.UtcNow;

    public override bool Equals(object? obj)
    {
        if (obj is not BaseEntity<TId> other || GetType() != other.GetType())
            return false;
        return EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    public override int GetHashCode() => Id?.GetHashCode() ?? 0;
}