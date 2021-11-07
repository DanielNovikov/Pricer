using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Model.Data;

namespace PriceObserver.Data.Cache
{
    public class MenuRepositoryCache : IMenuRepository
    {   
        private readonly IMemoryCache _cache;
        private readonly IMenuRepository _repository;

        public MenuRepositoryCache(
            IMemoryCache cache, 
            IMenuRepository repository)
        {
            _cache = cache;
            _repository = repository;
        }
        
        public Task<Menu> GetDefault()
        {
            return _cache.GetOrCreateAsync(
                $"{nameof(Menu)}_default",
                (entry) => _repository.GetDefault());
        }
    }
}