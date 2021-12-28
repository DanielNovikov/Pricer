using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;

namespace PriceObserver.Data.Service.Concrete;

public class ResourceService : IResourceService
{
    private readonly IResourceRepository _resourceRepository;

    public ResourceService(IResourceRepository resourceRepository)
    {
        _resourceRepository = resourceRepository;
    }

    public string Get(ResourceKey key, params object[] parameters)
    {
        var resource = _resourceRepository.GetByKey(key);

        return string.Format(resource.Value, parameters);
    }
}