using System.Collections.Generic;
using System.Threading.Tasks;
using PriceObserver.Data.Persistent.Models;

namespace PriceObserver.Data.Persistent.Repositories.Abstract;

public interface IAppNotificationRepository : IRepository<AppNotification>
{
    Task<IList<AppNotification>> GetToExecute();
}