
namespace Domain.Common.Primitives;

public sealed class Error(
    string code,
    string message)
    : ValueObject
{
    public string Code { get; private set; } = code;

    public string Message { get; private set; } = message;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
        yield return Message;
    }

    internal static Error None => new(string.Empty, string.Empty);
}
