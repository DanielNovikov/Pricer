using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Models
{
    public class Resource
    {
        public Resource(ResourceKey key, string value)
        {
            Key = key;
            Value = value;
        }
        
        public ResourceKey Key { get; }
        
        public string Value { get; }
    }
}