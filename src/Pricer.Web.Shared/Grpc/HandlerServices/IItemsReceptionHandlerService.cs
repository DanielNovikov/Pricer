using Pricer.Web.Shared.Grpc;

namespace Pricer.Web.Shared.Grpc.HandlerServices;

public interface IItemsReceptionHandlerService
{
    Task<ItemsReceptionReply> Receive(int userId);
}