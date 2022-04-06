using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Web.Api.Services.Abstract;
using PriceObserver.Web.Shared.Models;

namespace PriceObserver.Web.Api.Services.Concrete;

public class ShopVmBuilder : IShopVmBuilder
{
    private readonly IResourceService _resourceService;

    public ShopVmBuilder(IResourceService resourceService)
    {
        _resourceService = resourceService;
    }

    public ShopVm Build(Shop shop)
    {
        var address = _resourceService.Get(ResourceKey.Api_UrlTemplate, shop.Host);
        var logo = shop.Logo;
        var currencySign = _resourceService.Get(shop.Currency.Sign);
        var sameFormatImages = shop.SameFormatImages;
        
        return new ShopVm(address, logo, currencySign, sameFormatImages);
    }
}