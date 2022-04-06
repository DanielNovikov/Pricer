using PriceObserver.Web.Shared.Models;

namespace PriceObserver.Web.Shared.Services.Abstract;

public interface IItemService
{
    Task<IList<ItemsVm>> GetByUserId(long userId);
        
    Task Delete(int id, long userId);
}