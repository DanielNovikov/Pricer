using Microsoft.Extensions.Caching.Memory;
using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Repositories.Abstract;
using Pricer.Data.InMemory.Seed;

namespace Pricer.Data.InMemory.Repositories.Concrete;

public class ShopCategoryRepository : ReadOnlyRepositoryBase<ShopCategory>, IShopCategoryRepository
{
	public ShopCategoryRepository(IMemoryCache memoryCache) : base(memoryCache, CacheKey.ShopCategories) { }
}