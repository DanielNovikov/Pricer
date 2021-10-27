using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PriceObserver.Data.Repositories.Abstract;

namespace PriceObserver.Data.Repositories.Concrete
{
    public class MenuCommandRepository : IMenuCommandRepository
    {
        private readonly ObserverContext _context;

        public MenuCommandRepository(ObserverContext context)
        {
            _context = context;
        }

        public Task<bool> HasPair(int menuId, int commandId)
        {
            return _context.MenuCommands
                .AsNoTracking()
                .AnyAsync(x => x.MenuId == menuId && x.CommandId == commandId);
        }
    }
}