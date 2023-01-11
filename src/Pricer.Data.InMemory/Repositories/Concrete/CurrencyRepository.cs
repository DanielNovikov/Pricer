using Microsoft.Extensions.Caching.Memory;
using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.InMemory.Repositories.Abstract;
using Pricer.Data.InMemory.Seed;

namespace Pricer.Data.InMemory.Repositories.Concrete;

public class CurrencyRepository : ReadOnlyRepositoryBase<Currency, CurrencyKey>, ICurrencyRepository
{
	public CurrencyRepository(IMemoryCache memoryCache) : base(memoryCache, CacheKey.Currencies)
	{ }
}