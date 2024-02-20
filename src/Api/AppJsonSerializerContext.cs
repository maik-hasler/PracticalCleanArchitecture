using System.Text.Json.Serialization;
using Domain.Auctions;

namespace Api;

[JsonSerializable(typeof(IEnumerable<Auction>))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}
