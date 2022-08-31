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
        return await Context
            .Items
            .AsNoTracking()
            .Where(x => !x.IsDeleted)
            .ToListAsync();
    }

    public async Task<IList<Item>> GetByUserId(int userId)
    {
        return await Context
            .Items
            .AsNoTracking()
            .Include(x => x.PriceChanges)
            .Where(x => x.UserId == userId && !x.IsDeleted)
            .ToListAsync();
    }

    public async Task<IList<Item>> GetByUserIdWithLimit(int userId, int limit)
    {
        return await Context
            .Items
            .AsNoTracking()
            .Include(x => x.PriceChanges)
            .Where(x => x.UserId == userId && !x.IsDeleted)
            .OrderBy(x => x.Title)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<Item> GetByUserIdAndUrl(int userId, Uri url)
    {
        return await Context
            .Items
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId == userId && x.Url == url);
    }
}