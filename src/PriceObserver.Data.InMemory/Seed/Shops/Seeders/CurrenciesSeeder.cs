﻿using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using System.Collections.Generic;

namespace PriceObserver.Data.InMemory.Seed.Shops.Seeders;

public class CurrenciesSeeder
{
	public static List<Currency> Seed()
	{
		return new List<Currency>
		{
			new Currency(
				CurrencyKey.UAH,
				ResourceKey.Currency_UAH_Title,
				ResourceKey.Currency_UAH_Sign),
            
			new Currency(
				CurrencyKey.EUR,
				ResourceKey.Currency_EUR_Title,
				ResourceKey.Currency_EUR_Sign)
		};
	}
}