using System.Threading.Tasks;
using PriceObserver.Data.Models;

namespace PriceObserver.Data.Service.Abstract;

public interface IItemParseResultService
{
    Task CreateSucceeded(Item item);

    Task CreateFailed(Item item);
}