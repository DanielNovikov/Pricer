using PriceObserver.Web.Shared.Grpc;
using Pricer.Data.InMemory.Models;

namespace Pricer.Web.Api.Services.Abstract;

public interface IShopResponseModelBuilder
{
    ShopResponseModel Build(Shop shop);
}