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
			new Currency(
				CurrencyKey.UAH,
				ResourceKey.Currency_UAH_Title,
				ResourceKey.Currency_UAH_Sign),
            
			new Currency(
				CurrencyKey.EUR,
				ResourceKey.Currency_EUR_Title,
				ResourceKey.Currency_EUR_Sign),
            
			new Currency(
				CurrencyKey.USD,
				ResourceKey.Currency_USD_Title,
				ResourceKey.Currency_USD_Sign)
		};

		cache.Set(CacheKey.Currencies, currencies);
	}
}