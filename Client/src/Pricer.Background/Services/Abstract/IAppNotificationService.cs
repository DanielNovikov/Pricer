using System.Threading.Tasks;

namespace Pricer.Background.Services.Abstract;

public interface IAppNotificationService
{
    Task Execute();
}