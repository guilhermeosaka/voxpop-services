namespace Voxpop.Packages.Dispatcher.Types;

public class Result
{
    public bool IsSuccess { get; protected set; }
    public Error? Error { get; protected set; }

    protected Result()
    {
        IsSuccess = true;
    }
    
    private Result(Error error)
    {
        IsSuccess = false;
        Error = error;
    }
    
    public static Result Success() => new();
    
    public static implicit operator Result(Error error) => new(error);
}

public class Result<TResult> : Result
{
    public TResult? Value { get; }

    private Result(TResult value)
    {
        IsSuccess = true;
        Value = value;
    }

    private Result(Error error)
    {
        IsSuccess = false;
        Error = error;
    }
    
    public static implicit operator Result<TResult>(Error error) => new(error);
    public static implicit operator Result<TResult>(TResult value) => new(value);
}