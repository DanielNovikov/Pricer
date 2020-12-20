using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Model.Data;

namespace PriceObserver.Data.Repositories.Concrete
{
    public class ItemRepository : IItemRepository
    {
        private readonly ObserverContext _context;

        public ItemRepository(ObserverContext context)
        {
            _context = context;
        }

        public async Task<List<Item>> GetAll()
        {
            return await _context
                .Items
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Item> GetById(int id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item != null)
                _context.Entry(item).State = EntityState.Detached;

            return item;
        }

        public async Task<List<Item>> GetByUserId(long userId)
        {
            return await _context
                .Items
                .AsNoTracking()
                .Where(i => i.UserId == userId)
                .ToListAsync();
        }

        public async Task Add(Item item)
        {
            await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();
            
            _context.Entry(item).State = EntityState.Detached;
        }

        public async Task Update(Item item)
        {
            _context.Items.Update(item);
            await _context.SaveChangesAsync();
            
            _context.Entry(item).State = EntityState.Detached;
        }

        public async Task Delete(Item item)
        {
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}