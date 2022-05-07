using System;
using System.Linq;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Services.Abstract;

namespace PriceObserver.Dialog.Services.Concrete;

public class ShopsInfoMessageBuilder : IShopsInfoMessageBuilder
{
    private readonly IShopRepository _shopRepository;
    private readonly IResourceService _resourceService;

    public ShopsInfoMessageBuilder(
        IShopRepository shopRepository,
        IResourceService resourceService)
    {
        _shopRepository = shopRepository;
        _resourceService = resourceService;
    }

    public string Build(int? limit = default)
    {
        var shops = _shopRepository.GetAll(limit);
        
        var shopsInfo = shops
            .OrderBy(x => x.Name)
            .Select(x => $"- {x.Name} ({x.Host})")
            .Aggregate((x, y) => $"{x}{Environment.NewLine}{y}");

        if (limit.HasValue)
            shopsInfo += $"{Environment.NewLine}- ...";

        return _resourceService.Get(ResourceKey.Dialog_AvailableShops, shopsInfo);
    }
}