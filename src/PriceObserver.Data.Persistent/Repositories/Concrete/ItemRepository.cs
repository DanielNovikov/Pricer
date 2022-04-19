using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Data.Persistent.Repositories.Abstract;

namespace PriceObserver.Data.Persistent.Repositories.Concrete;

public class ItemRepository : IItemRepository
{
    private readonly ApplicationDbContext _context;

    public ItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IList<Item>> GetAll()
    {
        return await _context
            .Items
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Item> GetById(int id)
    {
        return await _context
            .Items
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IList<Item>> GetByUserId(long userId)
    {
        return await _context
            .Items
            .AsNoTracking()
            .Include(x => x.PriceChanges)
            .Where(i => i.UserId == userId)
            .ToListAsync();
    }

    public async Task<IList<Item>> GetByUserIdWithLimit(long userId, int limit)
    {
        return await _context
            .Items
            .AsNoTracking()
            .Include(x => x.PriceChanges)
            .Where(i => i.UserId == userId)
            .OrderBy(x => x.Title)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<bool> ExistsForUserByUrl(long userId, Uri url)
    {
        return await _context
            .Items
            .AsNoTracking()
            .AnyAsync(x => x.UserId == userId && x.Url == url);
    }

    public async Task Add(Item item)
    {
        await _context.Items.AddAsync(item);
        await _context.SaveChangesAsync();
            
        _context.DetachEntity(item);
    }

    public async Task Update(Item item)
    {
        _context.Items.Update(item);
        await _context.SaveChangesAsync();
            
        _context.DetachEntity(item);
    }

    public async Task Delete(Item item)
    {
        _context.Items.Remove(item);
        await _context.SaveChangesAsync();
    }
}