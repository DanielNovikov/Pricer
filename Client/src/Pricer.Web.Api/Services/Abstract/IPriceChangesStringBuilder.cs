using Pricer.Data.Persistent.Models;

namespace Pricer.Web.Api.Services.Abstract;

public interface IPriceChangesStringBuilder
{
    string Build(IList<ItemPriceChange> priceChanges);
}