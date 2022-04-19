using PriceObserver.Data.Persistent.Models;
using PriceObserver.Web.Shared.Grpc;

namespace PriceObserver.Web.Api.Services.Abstract;

public interface IItemResponseModelBuilder
{
    ItemResponseModel Build(Item item);
}