using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Data.Persistent.Repositories.Abstract;

namespace PriceObserver.Data.Persistent.Repositories.Concrete;

public class ItemParseResultRepository : IItemParseResultRepository
{
    private readonly ApplicationDbContext _context;

    public ItemParseResultRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ItemParseResult> GetLastByItemId(int itemId)
    {
        return await _context.ItemParseResults
            .AsNoTracking()
            .OrderBy(x => x.Created)
            .LastOrDefaultAsync(x => x.ItemId == itemId);
    }

    public async Task Add(ItemParseResult entity)
    {
        await _context.ItemParseResults.AddAsync(entity);
        await _context.SaveChangesAsync();
        
        _context.DetachEntity(entity);
    }
}