using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Model.Data;

namespace PriceObserver.Data.Repositories.Cache
{
    public class MenuCommandRepositoryCache : IMenuCommandRepository
    {
        private readonly IMemoryCache _cache;
        private readonly IMenuCommandRepository _repository;

        public MenuCommandRepositoryCache(
            IMemoryCache cache, 
            IMenuCommandRepository repository)
        {
            _cache = cache;
            _repository = repository;
        }
        
        public Task<bool> HasPair(int menuId, int commandId)
        {
            return _cache.GetOrCreateAsync(
                $"{nameof(MenuCommand)}_has_pair_{menuId}_{commandId}",
                (entry) => _repository.HasPair(menuId, commandId));
        }

        public Task<IList<Command>> GetCommandsByMenuId(int menuId)
        {
            return _cache.GetOrCreateAsync(
                $"{nameof(MenuCommand)}_commands_by_menu_id_{menuId}",
                (entry) => _repository.GetCommandsByMenuId(menuId));
        }
    }
}