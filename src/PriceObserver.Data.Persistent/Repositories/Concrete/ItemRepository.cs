using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Data.Persistent.Repositories.Abstract;

namespace PriceObserver.Data.Persistent.Repositories.Concrete;

public class ItemRepository : RepositoryBase<Item>, IItemRepository
{
    public ItemRepository(ApplicationDbContext context) : base(context)
    { }

    public override async Task<IList<Item>> GetAll()
    {
        return await Context.Items
            .AsNoTracking()
            .Include(x => x.User)
            .ToListAsync();
    }

    public async Task<IList<Item>> GetByUserId(int userId)
    {
        return await Context
            .Items
            .AsNoTracking()
            .Include(x => x.PriceChanges)
            .Where(i => i.UserId == userId)
            .ToListAsync();
    }

    public async Task<IList<Item>> GetByUserIdWithLimit(int userId, int limit)
    {
        return await Context
            .Items
            .AsNoTracking()
            .Include(x => x.PriceChanges)
            .Where(i => i.UserId == userId)
            .OrderBy(x => x.Title)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<bool> ExistsByUserIdAndUrl(int userId, Uri url)
    {
        return await Context
            .Items
            .AsNoTracking()
            .AnyAsync(x => x.UserId == userId && x.Url == url);
    }
}