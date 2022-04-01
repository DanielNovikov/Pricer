using System.Linq;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Models;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Web.Api.Models;
using PriceObserver.Web.Api.Services.Abstract;

namespace PriceObserver.Web.Api.Services.Concrete;

public class ItemVmBuilder : IItemVmBuilder
{
    private readonly IPriceChangesStringBuilder _priceChangesStringBuilder;
    private readonly IResourceService _resourceService;

    public ItemVmBuilder(
        IPriceChangesStringBuilder priceChangesStringBuilder,
        IResourceService resourceService)
    {
        _priceChangesStringBuilder = priceChangesStringBuilder;
        _resourceService = resourceService;
    }

    public ItemVm Build(Item item)
    {
        var priceChanges = item.PriceChanges.Any() 
            ? _priceChangesStringBuilder.Build(item.PriceChanges)
            : _resourceService.Get(ResourceKey.Api_NoHistory);

        return new ItemVm(
            item.Id,
            item.Title,
            item.Price,
            item.Url.ToString(),
            item.ImageUrl.ToString(),
            priceChanges);
    }
}