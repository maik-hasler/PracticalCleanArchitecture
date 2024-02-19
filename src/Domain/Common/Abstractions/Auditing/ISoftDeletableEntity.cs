namespace Domain.Common.Abstractions.Auditing;

public interface ISoftDeletableEntity
{
    bool Deleted { get; }

    DateTimeOffset? DeletedOn { get; }
}
