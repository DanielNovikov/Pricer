using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PriceObserver.Data.Models;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Data.Repositories.Abstract;

namespace PriceObserver.Data.Repositories.Concrete
{
    public class ShopRepository : IShopRepository
    {
        private readonly ApplicationDbContext _context;

        public ShopRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<Shop> GetByType(ShopType type)
        {
            return _context.Shops
                .AsNoTracking()
                .SingleAsync(x => x.Type == type);
        }

        public Task<Shop> GetByHost(string host)
        {
            return _context.Shops
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Host == host);
        }

        public async Task<IList<Shop>> GetAll()
        {
            return await _context.Shops
                .AsNoTracking()
                .ToListAsync();
        }
    }
}