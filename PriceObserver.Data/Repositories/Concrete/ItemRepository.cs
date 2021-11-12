﻿using System;
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
        private readonly ApplicationDbContext _context;

        public ItemRepository(ApplicationDbContext context)
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
            return await _context
                .Items
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Item>> GetByUserId(long userId)
        {
            return await _context
                .Items
                .AsNoTracking()
                .Include(x => x.Shop)
                .Where(i => i.UserId == userId)
                .ToListAsync();
        }

        public async Task<bool> ExistsByUrl(Uri url)
        {
            return await _context
                .Items
                .AsNoTracking()
                .AnyAsync(x => x.Url == url);
        }

        public async Task Add(Item item)
        {
            await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();
            
            _context.DetachAll();
        }

        public async Task Update(Item item)
        {
            _context.Items.Update(item);
            await _context.SaveChangesAsync();
            
            _context.DetachAll();
        }

        public async Task Delete(Item item)
        {
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}