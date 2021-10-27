using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Model.Data;
using PriceObserver.Model.Data.Enums;

namespace PriceObserver.Data.Repositories.Concrete
{
    public class MenuRepository : IMenuRepository
    {
        private readonly ObserverContext _context;

        public MenuRepository(ObserverContext context)
        {
            _context = context;
        }

        public Task<Menu> GetByType(MenuType type)
        {
            return _context.Menus
                .AsNoTracking()
                .SingleAsync(x => x.Type == type);
        }

        public Task<Menu> GetDefault()
        {
            return _context.Menus
                .AsNoTracking()
                .SingleAsync(x => x.IsDefault);
        }
    }
}