using PriceObserver.Api.Services.Abstract;
using PriceObserver.Api.Services.Models.Response;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Models;
using PriceObserver.Data.Service.Abstract;

namespace PriceObserver.Api.Services.Concrete;

public class ShopVmBuilder : IShopVmBuilder
{
    private readonly IResourceService _resourceService;
    private readonly IPriceChangesStringBuilder _priceChangesStringBuilder;

    public ShopVmBuilder(
        IResourceService resourceService, 
        IPriceChangesStringBuilder priceChangesStringBuilder)
    {
        _resourceService = resourceService;
        _priceChangesStringBuilder = priceChangesStringBuilder;
    }

    public ShopVm Build(Shop shop, IList<Item> items)
    {
        var address = _resourceService.Get(ResourceKey.Api_UrlTemplate, shop.Host);
        var logoFileName = shop.LogoFileName;
        var currencySign = _resourceService.Get(shop.Currency.Sign);
        
        var itemVMs = items
            .Select(y =>
            {
                var priceChanges = y.PriceChanges.Any() 
                    ? _priceChangesStringBuilder.Build(y.PriceChanges)
                    : _resourceService.Get(ResourceKey.Api_NoHistory);

                return y.ToVm(priceChanges);
            })
            .ToList();

        return new ShopVm(address, logoFileName, currencySign, itemVMs);
    }
}