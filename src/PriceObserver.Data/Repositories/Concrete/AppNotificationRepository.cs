using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PriceObserver.Data.Models;
using PriceObserver.Data.Repositories.Abstract;

namespace PriceObserver.Data.Repositories.Concrete;

public class AppNotificationRepository : IAppNotificationRepository
{
    private readonly ApplicationDbContext _context;

    public AppNotificationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IList<AppNotification>> GetToExecute()
    {
        return await _context.AppNotifications
            .AsNoTracking()
            .Where(x => !x.Executed)
            .ToListAsync();
    }

    public async Task Update(AppNotification appNotification)
    {
        _context.AppNotifications.Update(appNotification);
        await _context.SaveChangesAsync();
            
        _context.DetachAll();
    }
}