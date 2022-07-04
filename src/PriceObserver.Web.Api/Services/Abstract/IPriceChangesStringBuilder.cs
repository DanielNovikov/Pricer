﻿using PriceObserver.Data.Persistent.Models;

namespace PriceObserver.Web.Api.Services.Abstract;

public interface IPriceChangesStringBuilder
{
    string Build(IList<ItemPriceChange> priceChanges);
}