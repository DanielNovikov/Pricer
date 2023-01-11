using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pricer.Data.Persistent.Models;
using Pricer.Data.Persistent.Repositories.Abstract;

namespace Pricer.Data.Persistent.Repositories.Concrete;

public class AppNotificationRepository : RepositoryBase<AppNotification>, IAppNotificationRepository
{
    public AppNotificationRepository(ApplicationDbContext context) : base(context)
    { }

    public async Task<IList<AppNotification>> GetToExecute()
    {
        return await Context.AppNotifications
            .AsNoTracking()
            .Where(x => !x.Executed)
            .ToListAsync();
    }
}