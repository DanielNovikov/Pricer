using System.Collections.Generic;
using System.Threading.Tasks;
using PriceObserver.Model.Service;

namespace PriceObserver.Data.Service.Abstract
{
    public interface IItemService
    {
        Task<IList<ShopWithItemsVM>> GetGroupedByUserId(long userId);

        Task Delete(int id);
    }
}