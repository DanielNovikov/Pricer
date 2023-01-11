using PriceObserver.Data.Persistent.Models;
using PriceObserver.Web.Shared.Grpc;

namespace Pricer.Web.Api.Services.Abstract;

public interface IItemResponseModelBuilder
{
    ItemResponseModel Build(Item item);
}