using PriceObserver.Data.Models.Enums;

namespace PriceObserver.Data.Models
{
    public class Resource
    {
        public int Id { get; set; }
        
        public ResourceKey Key { get; set; }
        
        public string Value { get; set; }
    }
}