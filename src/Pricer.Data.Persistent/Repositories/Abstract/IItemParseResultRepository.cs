using System.Threading.Tasks;
using Pricer.Data.Persistent.Models;

namespace Pricer.Data.Persistent.Repositories.Abstract;

public interface IItemParseResultRepository : IRepository<ItemParseResult>
{
    Task<ItemParseResult> GetLastSucceededByItemId(int itemId);
    
    Task<int> GetCountOfFailedByItemId(int id, int itemId);
}