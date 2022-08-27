using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PriceObserver.Data.Persistent.Models;

namespace PriceObserver.Data.Persistent.Repositories.Abstract;

public interface IItemRepository : IRepository<Item>
{
    Task<IList<Item>> GetByUserId(int userId);
    
    Task<IList<Item>> GetByUserIdWithLimit(int userId, int limit);

    Task<Item> GetByUserIdAndUrl(int userId, Uri url);
}