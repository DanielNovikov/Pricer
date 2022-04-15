using PriceObserver.Data.InMemory.Models;
using PriceObserver.Web.Shared.Grpc;

namespace PriceObserver.Web.Api.Services.Abstract;

public interface IShopResponseModelBuilder
{
    ShopResponseModel Build(Shop shop);
}