namespace PriceObserver.Common.Models;

public class ServiceSuccessResult<TImplementation, TResult> 
	where TImplementation : ServiceSuccessResult<TImplementation, TResult>, new()
{   
	public bool IsSuccess { get; private init; }

	public TResult Result { get; private init; }
        
	public static TImplementation Success(TResult result)
	{
		return new TImplementation
		{
			IsSuccess = true,
			Result = result
		};
	}

	public static TImplementation Fail()
	{
		return new TImplementation
		{
			IsSuccess = false
		};
	}
}