using PriceObserver.Model.Data.Enums;

namespace PriceObserver.Model.Parser
{
    public class ParsedItem
    {
        public ShopType ShopType { get; set; }
        
        public int Price { get; set; }
        
        public string Title { get; set; }
    }
}