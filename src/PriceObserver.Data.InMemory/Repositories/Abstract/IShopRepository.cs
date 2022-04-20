using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Repositories.Abstract;

public interface IShopRepository : IReadOnlyRepository<Shop, ShopKey>
{
    Shop GetByHost(string host);
}