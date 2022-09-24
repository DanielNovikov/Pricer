using System.Collections.Generic;
using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Repositories.Abstract;

public interface IShopRepository : IReadOnlyRepository<Shop, ShopKey>
{
    IEnumerable<Shop> GetAll(int? limit);
    
    Shop GetByHost(string host);
}