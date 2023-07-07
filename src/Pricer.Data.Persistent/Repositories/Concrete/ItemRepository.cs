using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Models;
using Pricer.Data.Persistent.Repositories.Abstract;

namespace Pricer.Data.Persistent.Repositories.Concrete;

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

    public async Task<CurrencyKey> GetCurrency(int id)
    {
        return (await GetById(id)).CurrencyKey;
    }

    public async Task<bool> Any(int userId)
    {
        return await Context
            .Items
            .AsNoTracking()
            .Where(x => !x.IsDeleted)
            .AnyAsync(x => x.UserId == userId);
    }
}