using System.Threading.Tasks;

namespace PriceObserver.Background.Services.Abstract;

public interface IAppNotificationService
{
    Task Execute();
}