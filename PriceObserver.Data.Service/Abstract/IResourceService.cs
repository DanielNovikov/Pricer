using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.Service.Abstract;

public interface IResourceService
{
    string Get(ResourceKey key, params object[] parameters);
}