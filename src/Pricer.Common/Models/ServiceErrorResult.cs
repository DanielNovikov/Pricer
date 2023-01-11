namespace Pricer.Common.Models;

public class ServiceErrorResult<TImplementation, TError> 
    where TImplementation : ServiceErrorResult<TImplementation, TError>, new()
{   
    public bool IsSuccess { get; private init; }

    public TError Error { get; private init; }
        
    public static TImplementation Success()
    {
        return new TImplementation
        {
            IsSuccess = true
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