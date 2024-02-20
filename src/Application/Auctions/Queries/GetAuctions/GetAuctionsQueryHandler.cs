using Application.Common.Abstractions.Messaging;
using Domain.Auctions;
using Domain.Common.Primitives;

namespace Application.Auctions.Queries.GetAuctions;

internal sealed class GetAuctionsQueryHandler
    : IQueryHandler<GetAuctionsQuery, IEnumerable<Auction>>
{
    public ValueTask<Result<IEnumerable<Auction>>> Handle(
        GetAuctionsQuery request,
        CancellationToken cancellationToken)
    {
        var auctions = new List<Auction>
        {
            new Auction()
        };

        Thread.Sleep(501);

        return ValueTask.FromResult(
            Result<IEnumerable<Auction>>.Success(auctions));
    }
}
