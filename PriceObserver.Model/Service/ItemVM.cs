using System;

namespace PriceObserver.Model.Service
{
    public class ItemVM
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public int Price { get; set; }
        
        public Uri Url { get; set; }
        
        public Uri ImageUrl { get; set; }
    }
}