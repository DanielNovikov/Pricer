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

    public string Build()
    {
        var shops = _shopRepository.GetAll();
            
        var shopsInfo = shops
            .Select(x => $"- {x.Name} ({x.Host})")
            .Aggregate((x, y) => $"{x}{Environment.NewLine}{y}");

        return _resourceService.Get(ResourceKey.Dialog_AvailableShops, shopsInfo);
    }
}