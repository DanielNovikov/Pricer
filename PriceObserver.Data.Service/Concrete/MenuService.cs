using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;

namespace PriceObserver.Data.Service.Concrete;

public class MenuService : IMenuService
{
    private readonly IMenuRepository _repository;
    private readonly IResourceRepository _resourceRepository;

    public MenuService(
        IMenuRepository repository, 
        IResourceRepository resourceRepository)
    {
        _repository = repository;
        _resourceRepository = resourceRepository;
    }

    public string GetTitle(MenuKey key)
    {
        var menu = _repository.GetByKey(key);
        var resource =  _resourceRepository.GetByKey(menu.ResourceKey);

        return resource.Value;
    }
}