using System.Collections.Generic;
using System.Threading.Tasks;
using PriceObserver.Data.Models;
using PriceObserver.Data.Models.Enums;

namespace PriceObserver.Data.Repositories.Abstract
{
    public interface IShopRepository
    {
        Task<Shop> GetByType(ShopType type);
        
        Task<Shop> GetByHost(string host);

        Task<IList<Shop>> GetAll();
    }
}