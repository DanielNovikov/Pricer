using PriceObserver.Api.Models.Response;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.Models;

namespace PriceObserver.Api.Services.Abstract;

public interface IShopVmBuilder
{
    ShopVm Build(Shop shop, IList<Item> items);
}