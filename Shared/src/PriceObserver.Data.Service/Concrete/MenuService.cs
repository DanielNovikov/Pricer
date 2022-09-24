using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;

namespace PriceObserver.Data.Service.Concrete;

public class MenuService : IMenuService
{
    private readonly IMenuRepository _repository;
    private readonly IResourceService _resourceService;

    public MenuService(
        IMenuRepository repository, 
        IResourceService resourceService)
    {
        _repository = repository;
        _resourceService = resourceService;
    }

    public string GetTitle(MenuKey key)
    {
        var menu = _repository.GetByKey(key);
        return _resourceService.Get(menu.Title);;
    }
}