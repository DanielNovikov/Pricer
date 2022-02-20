using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PriceObserver.Data.Models;

namespace PriceObserver.Data.Repositories.Abstract;

public interface IItemRepository
{
    Task<IList<Item>> GetAll();
        
    Task<Item> GetById(int id);

    Task<IList<Item>> GetByUserId(long userId);
    
    Task<IList<Item>> GetByUserIdWithLimit(long userId, int limit);

    Task<bool> ExistsForUserByUrl(long userId, Uri url);
        
    Task Add(Item item);

    Task Update(Item item);
        
    Task Delete(Item item);
}