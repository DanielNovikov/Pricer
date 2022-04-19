using System.Collections.Generic;
using System.Threading.Tasks;
using PriceObserver.Data.Persistent.Models;

namespace PriceObserver.Data.Persistent.Repositories.Abstract;

public interface IAppNotificationRepository
{
    Task<IList<AppNotification>> GetToExecute();

    Task Update(AppNotification appNotification);
}