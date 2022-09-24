using PriceObserver.Web.Shared.Grpc;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Models;
using Pricer.Data.Service.Abstract;
using Pricer.Web.Api.Services.Abstract;

namespace Pricer.Web.Api.Services.Concrete;

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