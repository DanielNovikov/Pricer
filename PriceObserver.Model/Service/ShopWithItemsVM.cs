using System.Collections.Generic;

namespace PriceObserver.Model.Service
{
    public class ShopWithItemsVM
    {
        public string Title { get; set; }
        
        public string Host { get; set; }
        
        public IList<ItemVM> Items { get; set; }
    }
}