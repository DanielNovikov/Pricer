using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Model.Data;

namespace PriceObserver.Data.Cache
{
    public class ShopRepositoryCache : IShopRepository
    {
        private readonly IMemoryCache _cache;
        private readonly IShopRepository _repository;

        public ShopRepositoryCache(
            IMemoryCache cache, 
            IShopRepository repository)
        {
            _cache = cache;
            _repository = repository;
        }

        public Task<Shop> GetByHost(string host)
        {
            return _cache.GetOrCreateAsync(
                $"{nameof(Shop)}_by_host_{host}",
                (entry) => _repository.GetByHost(host));
        }

        public Task<IList<Shop>> GetAll()
        {
            return _cache.GetOrCreateAsync(
                $"{nameof(Shop)}_all",
                (entry) => _repository.GetAll());
        }
    }
}