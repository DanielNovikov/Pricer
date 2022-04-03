namespace PriceObserver.Web.Shared.Services.Abstract;

public interface ICookieManager
{
    Task<string?> GetValue(string key);
    
    Task SetValue(string key, string value);

    Task Remove(string key);
}