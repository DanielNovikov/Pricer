using System;

namespace PriceObserver.Model.Service
{
    public class ItemVM
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public int Price { get; set; }
        
        public string Url { get; set; }
        
        public string ImageUrl { get; set; }
    }
}