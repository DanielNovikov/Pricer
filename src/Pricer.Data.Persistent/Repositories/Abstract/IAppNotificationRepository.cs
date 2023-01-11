using System.Collections.Generic;
using System.Threading.Tasks;
using Pricer.Data.Persistent.Models;

namespace Pricer.Data.Persistent.Repositories.Abstract;

public interface IAppNotificationRepository : IRepository<AppNotification>
{
    Task<IList<AppNotification>> GetToExecute();
}