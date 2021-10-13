namespace PriceObserver.Model.Common
{
    public abstract class ServiceResult<TImplementation, TResult, TError> 
        where TImplementation : ServiceResult<TImplementation, TResult, TError>, new()
    {
        public bool IsSuccess { get; private set; }

        public TError Error { get; private set; }

        public TResult Result { get; private set; }

        public static TImplementation Success(TResult result)
        {
            return new TImplementation
            {
                IsSuccess = true,
                Result = result
            };
        }
        
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
        
        public static TImplementation Fail()
        {
            return new TImplementation
            {
                IsSuccess = false
            };
        }
    }
}