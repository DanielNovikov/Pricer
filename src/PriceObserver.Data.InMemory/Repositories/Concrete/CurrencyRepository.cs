using Microsoft.Extensions.Caching.Memory;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Data.InMemory.Seed;

namespace PriceObserver.Data.InMemory.Repositories.Concrete;

public class CurrencyRepository : ReadOnlyRepositoryBase<Currency, CurrencyKey>, ICurrencyRepository
{
	public CurrencyRepository(IMemoryCache memoryCache) : base(memoryCache, CacheKey.Currencies)
	{ }
}