using System.Threading.Tasks;
using PriceObserver.Data.Persistent.Models;

namespace PriceObserver.Data.Persistent.Repositories.Abstract;

public interface IItemParseResultRepository : IRepository<ItemParseResult>
{
    Task<ItemParseResult> GetLastSucceededByItemId(int itemId);
    
    Task<int> GetCountOfFailedByItemId(int id, int itemId);
}