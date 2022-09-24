using System.Threading.Tasks;

namespace PriceObserver.Background.Services.Abstract;

public interface IItemsObserverService
{
    Task Observe();
}