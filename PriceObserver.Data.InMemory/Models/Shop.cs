using System;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Models
{
    public class Shop
    {
        public Shop(string name, ShopKey key, string host, Uri logoUrl)
        {
            Name = name;
            Key = key;
            Host = host;
            LogoUrl = logoUrl;
        }
        
        public string Name { get; }
        
        public ShopKey Key { get; }
        
        public string Host { get; }
        
        public Uri LogoUrl { get; }
    }
}