using System.Collections.Generic;
using System.Threading.Tasks;
using Pricer.Data.Persistent.Models;

namespace Pricer.Data.Persistent.Repositories.Abstract;

public interface IItemPriceChangeRepository : IRepository<ItemPriceChange>
{
    Task<List<ItemPriceChange>> GetByItemId(int itemId);
    
    Task<List<ItemPriceChange>> GetByItemId(int itemId, int top);
}