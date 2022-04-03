using PriceObserver.Data.InMemory.Models;
using PriceObserver.Web.Shared.Models;

namespace PriceObserver.Web.Api.Services.Abstract;

public interface IShopVmBuilder
{
    ShopVm Build(Shop shop);
}