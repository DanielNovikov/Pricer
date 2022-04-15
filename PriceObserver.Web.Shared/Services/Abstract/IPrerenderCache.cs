namespace PriceObserver.Web.Shared.Services.Abstract;

public interface IPrerenderCache
{
    Task<TResult> GetOrAdd<TResult>(string key, Func<Task<TResult>> dataFactory);

    string Serialize();
}