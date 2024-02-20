using Application.Common.Abstractions.Messaging;
using Domain.Auctions;

namespace Application.Auctions.Queries.GetAuctions;

public sealed record GetAuctionsQuery
    : ICachableQuery<IEnumerable<Auction>>
{
    public string Key => "auctions";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(15);
}
