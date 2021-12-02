using System.Collections.Generic;
using System.Threading.Tasks;
using PriceObserver.Data.Models;

namespace PriceObserver.Data.Repositories.Abstract
{
    public interface INotificationRepository
    {
        Task<IList<Notification>> GetToExecute();

        Task Update(Notification notification);
    }
}