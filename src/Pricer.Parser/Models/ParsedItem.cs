using System;
using PriceObserver.Data.InMemory.Models.Enums;

namespace Pricer.Parser.Models;

public record ParsedItem
	(ShopKey ShopKey, int Price, string Title, Uri ImageUrl, bool IsAvailable, CurrencyKey CurrencyKey);