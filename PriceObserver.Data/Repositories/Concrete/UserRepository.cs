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
            var user = await _context.Users.FindAsync(id);

            if(user != null)
                _context.Entry(user).State = EntityState.Detached;

            return user;
        }

        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
            _context.Entry(user).State = EntityState.Detached;

            await _context.SaveChangesAsync();
        }
    }
}