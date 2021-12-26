using System;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Parser.Models
{
    public record ParsedItem(ShopKey ShopKey, int Price, string Title, Uri ImageUrl);
}