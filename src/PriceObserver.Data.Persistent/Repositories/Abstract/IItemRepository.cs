using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PriceObserver.Data.Persistent.Models;

namespace PriceObserver.Data.Persistent.Repositories.Abstract;

public interface IItemRepository
{
    Task<IList<Item>> GetAll();
        
    Task<Item> GetById(int id);

    Task<IList<Item>> GetByUserId(int userId);
    
    Task<IList<Item>> GetByUserIdWithLimit(int userId, int limit);

    Task<bool> ExistsByUserIdAndUrl(int userId, Uri url);
        
    Task Add(Item item);

    Task Update(Item item);
        
    Task Delete(Item item);
}