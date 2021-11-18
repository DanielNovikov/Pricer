using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PriceObserver.Data.Models;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Data.Repositories.Abstract;

namespace PriceObserver.Data.Repositories.Concrete
{
    public class CommandRepository : ICommandRepository
    {
        private readonly ApplicationDbContext _context;

        public CommandRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<Command> GetByTitle(string title)
        {
            return _context
                .Commands
                .AsNoTracking()
                .Include(x => x.MenuToRedirect)
                .SingleOrDefaultAsync(x => x.Title == title);
        }

        public Task<Command> GetByType(CommandType type)
        {
            return _context
                .Commands
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Type == type);
        }
    }
}