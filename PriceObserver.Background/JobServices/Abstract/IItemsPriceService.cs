using System.Threading.Tasks;

namespace PriceObserver.Background.JobServices.Abstract;

public interface IItemsPriceService
{
    Task Observe();
}