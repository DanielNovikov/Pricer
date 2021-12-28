using System.Collections.Generic;
using System.Threading.Tasks;
using PriceObserver.Data.Models;
using PriceObserver.Data.Service.Models;

namespace PriceObserver.Data.Service.Abstract;

public interface IItemService
{
    Task<IList<ShopVm>> GetGroupedByUserId(long userId);

    Task UpdatePrice(Item item, int price);
        
    Task Delete(int id, long userId);
}