using PriceObserver.Web.App.Services.Abstract;
using PriceObserver.Web.Shared.Models;
using PriceObserver.Web.Shared.Services.Abstract;

namespace PriceObserver.Web.App.Services.Concrete;

public class ItemHttpService : IItemService
{
    private readonly IAuthorizedHttpClient _authorizedHttpClient;
    
    private const string GetEndpoint = "api/items";
    private const string DeleteEndpoint = "api/item/{0}";
    
    public ItemHttpService(IAuthorizedHttpClient authorizedHttpClient)
    {
        _authorizedHttpClient = authorizedHttpClient;
    }
    
    public async Task<IList<ItemsVm>> GetByUserId(long userId)
    {
        return await _authorizedHttpClient.Get<List<ItemsVm>>(GetEndpoint);
    }

    public async Task Delete(int id, long userId)
    {
        var endpoint = string.Format(DeleteEndpoint, id);
        await _authorizedHttpClient.Delete(endpoint);
    }
}