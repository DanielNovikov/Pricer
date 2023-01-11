using PriceObserver.Data.InMemory.Models;
using PriceObserver.Web.Shared.Grpc;

namespace Pricer.Web.Api.Services.Abstract;

public interface IShopResponseModelBuilder
{
    ShopResponseModel Build(Shop shop);
}