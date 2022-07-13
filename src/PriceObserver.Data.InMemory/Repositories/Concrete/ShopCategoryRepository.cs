using Microsoft.Extensions.Caching.Memory;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Data.InMemory.Seed;

namespace PriceObserver.Data.InMemory.Repositories.Concrete;

public class ShopCategoryRepository : ReadOnlyRepositoryBase<ShopCategory>, IShopCategoryRepository
{
	public ShopCategoryRepository(IMemoryCache memoryCache) : base(memoryCache, CacheKey.ShopCategories) { }
}