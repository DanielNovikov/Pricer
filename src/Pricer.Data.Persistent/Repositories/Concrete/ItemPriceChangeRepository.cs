using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pricer.Data.Persistent.Models;
using Pricer.Data.Persistent.Repositories.Abstract;

namespace Pricer.Data.Persistent.Repositories.Concrete;

public class ItemPriceChangeRepository : RepositoryBase<ItemPriceChange>, IItemPriceChangeRepository
{
    public ItemPriceChangeRepository(ApplicationDbContext context) : base(context)
    { }

    public async Task<List<ItemPriceChange>> GetByItemId(int itemId)
    {
        return await Context
            .ItemPriceChanges
            .AsNoTracking()
            .Where(x => x.ItemId == itemId)
            .ToListAsync();
    }

    public async Task<List<ItemPriceChange>> GetByItemId(int itemId, int top)
    {
        return await Context
            .ItemPriceChanges
            .AsNoTracking()
            .Where(x => x.ItemId == itemId)
            .OrderByDescending(x => x.Created)
            .Take(top)
            .OrderBy(x => x.Created)
            .ToListAsync();
    }
}