using PriceObserver.Web.Shared.Grpc;
using Pricer.Data.Persistent.Models;

namespace Pricer.Web.Api.Services.Abstract;

public interface IItemResponseModelBuilder
{
    ItemResponseModel Build(Item item);
}