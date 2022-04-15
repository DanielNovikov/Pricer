using PriceObserver.Data.Models;
using PriceObserver.Web.Shared.Grpc;

namespace PriceObserver.Web.Api.Services.Abstract;

public interface IItemResponseModelBuilder
{
    ItemResponseModel Build(Item item);
}