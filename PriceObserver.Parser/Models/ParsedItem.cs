using System;
using PriceObserver.Data.Models.Enums;

namespace PriceObserver.Parser.Models
{
    public class ParsedItem
    {
        public ShopType ShopType { get; set; }
        
        public int Price { get; set; }
        
        public string Title { get; set; }
        
        public Uri ImageUrl { get; set; }
    }
}