using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Model.Data;
using PriceObserver.Model.Data.Enums;

namespace PriceObserver.Data.Cache
{
    public class CommandRepositoryCache : ICommandRepository
    {
        private readonly IMemoryCache _cache;
        private readonly ICommandRepository _repository;

        public CommandRepositoryCache(
            IMemoryCache cache, 
            ICommandRepository repository)
        {
            _cache = cache;
            _repository = repository;
        }

        public Task<Command> GetByTitle(string title)
        {
            return _cache.GetOrCreateAsync(
                $"{nameof(Command)}_by_title_{title}",
                (entry) => _repository.GetByTitle(title));
        }

        public Task<Command> GetByType(CommandType type)
        {
            return _cache.GetOrCreateAsync(
                $"{nameof(Command)}_by_type_{type}",
                (entry) => _repository.GetByType(type));
        }
    }
}