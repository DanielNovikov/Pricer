﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Data.Persistent.Repositories.Abstract;

namespace PriceObserver.Data.Persistent.Repositories.Concrete;

public class ItemParseResultRepository : RepositoryBase<ItemParseResult>, IItemParseResultRepository
{
    public ItemParseResultRepository(ApplicationDbContext context) : base(context)
    { }

    public async Task<ItemParseResult> GetLastSucceededByItemId(int itemId)
    {
        return await Context.ItemParseResults
            .AsNoTracking()
            .OrderBy(x => x.Created)
            .LastOrDefaultAsync(x => x.ItemId == itemId && x.IsSuccess);
    }

    public async Task<int> GetCountOfFailedByItemId(int id, int itemId)
    {
        return await Context.ItemParseResults
            .AsNoTracking()
            .CountAsync(x => x.Id > id && x.ItemId == itemId && !x.IsSuccess);
    }
}