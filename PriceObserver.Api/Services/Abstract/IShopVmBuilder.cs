using PriceObserver.Api.Models.Response;
using PriceObserver.Data.InMemory.Models;

namespace PriceObserver.Api.Services.Abstract;

public interface IShopVmBuilder
{
    ShopVm Build(Shop shop);
}