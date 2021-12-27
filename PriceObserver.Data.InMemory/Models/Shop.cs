using System;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Models
{
    public record Shop(string Name, ShopKey Key, string Host, string LogoFileName);
}