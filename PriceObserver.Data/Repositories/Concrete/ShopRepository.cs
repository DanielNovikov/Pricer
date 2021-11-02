using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Model.Data;
using PriceObserver.Model.Data.Enums;

namespace PriceObserver.Data.Repositories.Concrete
{
    public class ShopRepository : IShopRepository
    {
        private readonly ApplicationDbContext _context;

        public ShopRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<Shop> GetByHost(string host)
        {
            return _context.Shops
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Host == host);
        }
    }
}