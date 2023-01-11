using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Data.Service.Abstract;
using Pricer.Web.Api.Services.Abstract;

namespace Pricer.Web.Api.Services.Concrete;

public class PriceChangesStringBuilder : IPriceChangesStringBuilder
{
    private readonly IResourceService _resourceService;

    public PriceChangesStringBuilder(IResourceService resourceService)
    {
        _resourceService = resourceService;
    }

    public string Build(IList<ItemPriceChange> priceChanges)
    {
        return priceChanges
            .OrderBy(x => x.Created)
            .Select(z =>
            {
                var signResourceKey = z.OldPrice < z.NewPrice 
                    ? ResourceKey.Api_GrewUpSign 
                    : ResourceKey.Api_WentDownSign;

                var sign = _resourceService.Get(signResourceKey);

                return $"{z.OldPrice} {sign}";
            })
            .Aggregate((a, b) => $"{a} {b}");
    }
}