using Domain.Common.Abstractions.Auditing;
using Domain.Common.Primitives;

namespace Domain.Auctions;

public sealed class Auction
    : AggregateRoot<AuctionId>,
    IAuditableEntity,
    ISoftDeletableEntity
{
    public DateTimeOffset CreatedOn { get; private set; }

    public DateTimeOffset? ModifiedOn { get; private set; }

    public bool Deleted { get; private set; }

    public DateTimeOffset? DeletedOn { get; private set; }
}
