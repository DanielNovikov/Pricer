using System.Collections.Generic;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Repositories.Abstract
{
    public interface IShopRepository
    {
        Shop GetByKey(ShopKey key);
        
        Shop GetByHost(string host);

        IList<Shop> GetAll();
    }
}