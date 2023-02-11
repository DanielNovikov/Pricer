using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.Service.Abstract;

public interface IResourceService
{
    string Get(ResourceKey key, params object[] parameters);
    
    string GetDefault(ResourceKey key, params object[] parameters);
}