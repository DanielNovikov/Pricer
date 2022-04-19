using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Data.Persistent.Repositories.Abstract;

namespace PriceObserver.Data.Persistent.Repositories.Concrete;

public class ItemPriceChangeRepository : IItemPriceChangeRepository
{
    private readonly ApplicationDbContext _context;

    public ItemPriceChangeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Add(ItemPriceChange itemPriceChange)
    {
        _context.ItemPriceChanges.Add(itemPriceChange);
        await _context.SaveChangesAsync();

        _context.Entry(itemPriceChange).State = EntityState.Detached;
    }
}