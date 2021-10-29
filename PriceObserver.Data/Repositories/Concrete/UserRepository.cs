using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Model.Data;

namespace PriceObserver.Data.Repositories.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly ObserverContext _context;

        public UserRepository(ObserverContext context)
        {
            _context = context;
        }

        public async Task<User> GetById(long id)
        {
            return await _context.Users
                .AsNoTracking()
                .Include(x => x.Menu)
                .FirstOrDefaultAsync(x => x.Id == id);
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