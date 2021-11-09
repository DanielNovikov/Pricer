using System;

namespace PriceObserver.Model.Data
{
    public class Item
    {
        public int Id { get; set; }

        public Uri Url { get; set; }
        
        public int Price { get; set; }
        
        public string Title { get; set; }
        
        public Uri ImageUrl { get; set; }
        
        public long UserId { get; set; }
        public User User { get; set; }
        
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
    }
}