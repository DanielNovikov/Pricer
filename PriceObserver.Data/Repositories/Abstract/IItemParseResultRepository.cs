using System.Threading.Tasks;
using PriceObserver.Data.Models;

namespace PriceObserver.Data.Repositories.Abstract;

public interface IItemParseResultRepository
{
    Task<ItemParseResult> GetLastByItemId(int itemId);
    
    Task Add(ItemParseResult entity);
}