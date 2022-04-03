namespace PriceObserver.Web.App.Services.Abstract;

public interface IAuthorizedHttpClient
{
    Task<TType> Get<TType>(string endpoint);
    
    Task Delete(string endpoint);
}