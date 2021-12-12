using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PriceObserver.Data.Models;
using PriceObserver.Data.Repositories.Abstract;

namespace PriceObserver.Data.Repositories.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetById(long id)
        {
            return await _context.Users
                .AsNoTracking()
                .Include(x => x.Menu)
                    .ThenInclude(x => x.Parent)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IList<User>> GetAllActive()
        {
            return await _context.Users
                .AsNoTracking()
                .Where(x => x.IsActive)
                .ToListAsync();
        }

        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            
            _context.DetachAll();
        }

        public async Task Update(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            
            _context.DetachAll();
        }

        public async Task UpdateMenu(long id, Menu menu)
        {
            var user = await _context.Users.FindAsync(id);
            user.MenuId = menu.Id;

            await _context.SaveChangesAsync();
            _context.DetachAll();
        }
    }
}