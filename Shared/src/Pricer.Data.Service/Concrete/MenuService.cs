using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.InMemory.Repositories.Abstract;
using Pricer.Data.Service.Abstract;

namespace Pricer.Data.Service.Concrete;

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