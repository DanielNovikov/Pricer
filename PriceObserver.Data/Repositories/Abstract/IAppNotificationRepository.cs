using System.Collections.Generic;
using System.Threading.Tasks;
using PriceObserver.Data.Models;

namespace PriceObserver.Data.Repositories.Abstract
{
    public interface IAppNotificationRepository
    {
        Task<IList<AppNotification>> GetToExecute();

        Task Update(AppNotification appNotification);
    }
}