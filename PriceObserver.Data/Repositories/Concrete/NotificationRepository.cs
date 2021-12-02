using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PriceObserver.Data.Models;
using PriceObserver.Data.Repositories.Abstract;

namespace PriceObserver.Data.Repositories.Concrete
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Notification>> GetToExecute()
        {
            return await _context.Notifications
                .AsNoTracking()
                .Where(x => !x.Executed && x.Planned < DateTime.UtcNow)
                .ToListAsync();
        }

        public async Task Update(Notification notification)
        {
            _context.Notifications.Update(notification);
            await _context.SaveChangesAsync();
            
            _context.DetachAll();
        }
    }
}