using System.Threading.Tasks;

namespace PriceObserver.Background.JobServices.Abstract;

public interface IItemsObserverService
{
    Task Observe();
}