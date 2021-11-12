using System;

namespace PriceObserver.Model.Data
{
    public class ItemPriceChange
    {
        public int Id { get; set; }
        
        public DateTime Created { get; set; }
        
        public int OldPrice { get; set; }
        
        public int NewPrice { get; set; }
        
        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}