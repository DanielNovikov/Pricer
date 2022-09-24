namespace Pricer.Common.Models;

public abstract class ServiceResult<TImplementation, TResult, TError> 
    where TImplementation : ServiceResult<TImplementation, TResult, TError>, new()
{
    public bool IsSuccess { get; protected init; }

    public TError Error { get; protected init; }

    public TResult Result { get; protected init; }

    public static TImplementation Success(TResult result)
    {
        return new TImplementation
        {
            IsSuccess = true,
            Result = result
        };
    }

    public static TImplementation Fail(TError error)
    {
        return new TImplementation
        {
            IsSuccess = false,
            Error = error
        };
    }
}