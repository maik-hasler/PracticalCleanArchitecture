using System.Diagnostics.Contracts;

namespace Domain.Common.Primitives;

public class Result
{
    private readonly Error _error;

    public Error Error { get { return _error; } }

    public Result()
    {
        _error = Error.None;
    }

    public Result(
        Error error)
    {
        _error = error;
    }

    [Pure]
    public bool IsSuccess => _error == Error.None;

    [Pure]
    public bool IsFailure => !IsSuccess;

    public static Result Success() => new();

    public static Result Failure(Error error) => new(error);
}

public class Result<TValue>
{
    private readonly Error _error;

    private readonly TValue _value;

    public Error Error { get { return _error; } }

    public TValue Value { get { return _value; } }

    public Result(
        TValue value)
    {
        _value = value;
        _error = Error.None;
    }

    public Result(
        Error error)
    {
        _error = error;
        _value = default!;
    }

    [Pure]
    public bool IsSuccess => _error == Error.None;

    [Pure]
    public bool IsFailure => !IsSuccess;

    public static Result<TValue> Success(TValue value) => new(value);

    public static Result<TValue> Failure(Error error) => new(error);
}
