using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Web.Api.Services.Abstract;
using PriceObserver.Web.Shared.Grpc;

namespace PriceObserver.Web.Api.Services.Concrete;

public class ItemResponseModelBuilder : IItemResponseModelBuilder
{
    private readonly IPriceChangesStringBuilder _priceChangesStringBuilder;
    private readonly IResourceService _resourceService;
    private readonly ICurrencyService _currencyService;

    public ItemResponseModelBuilder(
        IPriceChangesStringBuilder priceChangesStringBuilder,
        IResourceService resourceService,
        ICurrencyService currencyService)
    {
        _priceChangesStringBuilder = priceChangesStringBuilder;
        _resourceService = resourceService;
        _currencyService = currencyService;
    }

    public ItemResponseModel Build(Item item)
    {
        var priceChanges = item.PriceChanges.Any() 
            ? _priceChangesStringBuilder.Build(item.PriceChanges)
            : _resourceService.Get(ResourceKey.Api_NoHistory);

        var currency = _currencyService.GetTitle(item.CurrencyKey);

        return new ItemResponseModel
        {
            Id = item.Id,
            Title = item.Title,
            Price = item.Price,
            Currency = currency,
            Url = item.Url.ToString(),
            ImageUrl = item.ImageUrl.ToString(),
            PriceChanges = priceChanges
        };
    }
}