using System.Threading.Tasks;
using PriceObserver.Data.Persistent.Models;

namespace PriceObserver.Data.Persistent.Repositories.Abstract;

public interface IItemParseResultRepository
{
    Task<ItemParseResult> GetLastByItemId(int itemId);
    
    Task Add(ItemParseResult entity);
}