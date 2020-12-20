namespace PriceObserver.Model.Parser
{
    public class ParsedItem
    {
        public ShopEnum Shop { get; set; }
        
        public int Price { get; set; }
        
        public bool IsItemOutOfStock { get; set; }
    }
}