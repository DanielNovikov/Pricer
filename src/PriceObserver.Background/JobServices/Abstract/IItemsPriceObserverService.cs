using System.Threading.Tasks;

namespace PriceObserver.Background.JobServices.Abstract;

public interface IItemsPriceObserverService
{
    Task Observe();
}