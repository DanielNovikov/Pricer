using System.Collections.Generic;
using PriceObserver.Data.Models;

namespace PriceObserver.Web.Api.Services.Abstract;

public interface IPriceChangesStringBuilder
{
    string Build(IList<ItemPriceChange> priceChanges);
}