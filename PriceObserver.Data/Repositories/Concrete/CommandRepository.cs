﻿using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Model.Data;
using PriceObserver.Model.Data.Enums;

namespace PriceObserver.Data.Repositories.Concrete
{
    public class CommandRepository : ICommandRepository
    {
        private readonly ObserverContext _context;

        public CommandRepository(ObserverContext context)
        {
            _context = context;
        }

        public Task<Command> GetByTitle(string title)
        {
            return _context
                .Commands
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Title == title);
        }

        public Task<Command> GetByType(CommandType type)
        {
            return _context
                .Commands
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Type == type);
        }
    }
}