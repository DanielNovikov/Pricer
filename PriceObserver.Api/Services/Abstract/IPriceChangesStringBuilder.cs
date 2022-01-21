using PriceObserver.Data.Models;

namespace PriceObserver.Api.Services.Abstract;

public interface IPriceChangesStringBuilder
{
    string Build(IList<ItemPriceChange> priceChanges);
}