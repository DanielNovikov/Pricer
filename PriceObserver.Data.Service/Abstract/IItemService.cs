using System.Collections.Generic;
using System.Threading.Tasks;
using PriceObserver.Model.Data;
using PriceObserver.Model.Service;

namespace PriceObserver.Data.Service.Abstract
{
    public interface IItemService
    {
        Task<IList<ShopWithItemsVM>> GetGroupedByUserId(long userId);

        Task UpdatePrice(Item item, int price);
        
        Task Delete(int id);
    }
}