using System;
using System.Collections.Generic;
using PriceObserver.Model.Data.Enums;

namespace PriceObserver.Model.Data
{
    public class Shop
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public ShopType Type { get; set; }
        
        public string Host { get; set; }
        
        public Uri LogoUrl { get; set; }
        
        public IList<Item> Items { get; set; }
    }
}