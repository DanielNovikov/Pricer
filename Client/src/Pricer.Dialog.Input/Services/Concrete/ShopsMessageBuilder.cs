using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.InMemory.Repositories.Abstract;
using Pricer.Data.Service.Abstract;
using Pricer.Dialog.Input.Services.Abstract;

namespace Pricer.Dialog.Input.Services.Concrete;

public class ShopsMessageBuilder : IShopsMessageBuilder
{
    private readonly IShopRepository _shopRepository;
    private readonly IResourceService _resourceService;

    public ShopsMessageBuilder(
        IShopRepository shopRepository,
        IResourceService resourceService)
    {
        _shopRepository = shopRepository;
        _resourceService = resourceService;
    }

    public string Build(int limit)
    {
        var shops = _shopRepository.GetAll(limit);
        
        var shopsInfo = shops
            .OrderBy(x => x.Name)
            .Select(x => $"- {x.Name} ({x.Host})")
            .Aggregate((x, y) => $"{x}{Environment.NewLine}{y}");

        shopsInfo += $"{Environment.NewLine}- ...";

        return _resourceService.Get(ResourceKey.Dialog_AvailableShops, shopsInfo);
    }
}