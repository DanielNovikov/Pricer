using System.Collections.Generic;

namespace PriceObserver.Web.Api.Models;

public record ItemsVm(ShopVm Shop, IList<ItemVm> Items);