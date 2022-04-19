using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Web.Api.Services.Abstract;
using PriceObserver.Web.Shared.Grpc;

namespace PriceObserver.Web.Api.Services.Concrete;

public class ItemResponseModelBuilder : IItemResponseModelBuilder
{
    private readonly IPriceChangesStringBuilder _priceChangesStringBuilder;
    private readonly IResourceService _resourceService;

    public ItemResponseModelBuilder(
        IPriceChangesStringBuilder priceChangesStringBuilder,
        IResourceService resourceService)
    {
        _priceChangesStringBuilder = priceChangesStringBuilder;
        _resourceService = resourceService;
    }

    public ItemResponseModel Build(Item item)
    {
        var priceChanges = item.PriceChanges.Any() 
            ? _priceChangesStringBuilder.Build(item.PriceChanges)
            : _resourceService.Get(ResourceKey.Api_NoHistory);

        return new ItemResponseModel
        {
            Id = item.Id,
            Title = item.Title,
            Price = item.Price,
            Url = item.Url.ToString(),
            ImageUrl = item.ImageUrl.ToString(),
            PriceChanges = priceChanges
        };
    }
}