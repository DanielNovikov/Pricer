using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PriceObserver.Data.Models;
using PriceObserver.Data.Repositories.Abstract;

namespace PriceObserver.Data.Repositories.Concrete
{
    public class MenuCommandRepository : IMenuCommandRepository
    {
        private readonly ApplicationDbContext _context;

        public MenuCommandRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<bool> HasPair(int menuId, int commandId)
        {
            return _context.MenuCommands
                .AsNoTracking()
                .AnyAsync(x => x.MenuId == menuId && x.CommandId == commandId);
        }

        public async Task<IList<Command>> GetCommandsByMenuId(int menuId)
        {
            return await _context.MenuCommands
                .AsNoTracking()
                .Include(x => x.Command)
                .Where(x => x.MenuId == menuId)
                .Select(x => x.Command)
                .ToListAsync();
        }
    }
}