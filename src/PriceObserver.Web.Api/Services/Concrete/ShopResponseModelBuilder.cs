using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Web.Api.Services.Abstract;
using PriceObserver.Web.Shared.Grpc;

namespace PriceObserver.Web.Api.Services.Concrete;

public class ShopResponseModelBuilder : IShopResponseModelBuilder
{
    private readonly IResourceService _resourceService;

    public ShopResponseModelBuilder(IResourceService resourceService)
    {
        _resourceService = resourceService;
    }

    public ShopResponseModel Build(Shop shop)
    {
        var address = _resourceService.Get(ResourceKey.Api_UrlTemplate, shop.Host);
        var logo = shop.Logo;
        var sameFormatImages = shop.SameFormatImages;
        
        return new ShopResponseModel
        {
            Address = address,
            Logo = logo,
            SameFormatImages = sameFormatImages
        };
    }
}