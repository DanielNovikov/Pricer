using System.Collections.Generic;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Repositories.Abstract;

public interface IShopRepository : IReadOnlyRepository<Shop, ShopKey>
{
    IEnumerable<Shop> GetAll(int? limit);
    
    Shop GetByHost(string host);
}