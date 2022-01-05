using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PriceObserver.Data.Models;
using PriceObserver.Data.Repositories.Abstract;

namespace PriceObserver.Data.Repositories.Concrete;

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
        
        _context.DetachAll();
    }
}