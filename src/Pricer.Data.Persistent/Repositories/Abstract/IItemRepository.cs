using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Models;

namespace Pricer.Data.Persistent.Repositories.Abstract;

public interface IItemRepository : IRepository<Item>
{
    Task<IList<Item>> GetByUserId(int userId);
    
    Task<IList<Item>> GetByUserIdWithLimit(int userId, int limit);

    Task<Item> GetByUserIdAndUrl(int userId, Uri url);

    Task<CurrencyKey> GetCurrency(int id);

    Task<bool> Any(int userId);
}