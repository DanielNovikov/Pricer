using System.Collections.Generic;

namespace PriceObserver.Model.Service
{
    public class ShopWithItemsVM
    {
        public string Address { get; set; }
     
        public string LogoUrl { get; set; }
        
        public IList<ItemVM> Items { get; set; }
    }
}