using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Seed.Currencies;

public class CurrenciesSeeder
{
	public static void Seed(IMemoryCache cache)
	{
		var currencies =  new List<Currency>
		{
			new Currency(CurrencyKey.UAH, " грн.", "₴"),
			new Currency(CurrencyKey.EUR, "€", "€"),
			new Currency(CurrencyKey.USD, "$", "$")
		};

		cache.Set(CacheKey.Currencies, currencies);
	}
}