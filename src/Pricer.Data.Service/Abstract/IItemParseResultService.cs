using System.Threading.Tasks;
using Pricer.Data.Persistent.Models;

namespace Pricer.Data.Service.Abstract;

public interface IItemParseResultService
{
    Task<int> GetLastErrorsCount(int itemId);
    
    Task CreateSucceeded(Item item);

    Task CreateFailed(Item item);
}