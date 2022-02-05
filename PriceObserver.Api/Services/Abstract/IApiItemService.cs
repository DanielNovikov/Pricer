using PriceObserver.Api.Models.Response;

namespace PriceObserver.Api.Services.Abstract;

public interface IApiItemService
{
    Task<IList<ItemsVm>> GetByUserId(long userId);
        
    Task Delete(int id, long userId);
}