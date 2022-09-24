using PriceObserver.Web.Shared.Grpc;
using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Service.Abstract;
using Pricer.Web.Api.Services.Abstract;

namespace Pricer.Web.Api.Services.Concrete;

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