using System;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Parser.Models
{
    public class ParsedItem
    {
        public ShopKey ShopKey { get; set; }
        
        public int Price { get; set; }
        
        public string Title { get; set; }
        
        public Uri ImageUrl { get; set; }
    }
}