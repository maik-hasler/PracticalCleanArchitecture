namespace Domain.Common.Abstractions.Auditing;

public interface IAuditableEntity
{
    DateTimeOffset CreatedOn { get; }

    DateTimeOffset? ModifiedOn { get; }
}
