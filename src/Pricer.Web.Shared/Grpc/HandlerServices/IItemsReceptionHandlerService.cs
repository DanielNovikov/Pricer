namespace Pricer.Web.Shared.Grpc.HandlerServices;

public interface IItemsReceptionHandlerService
{
    Task<ItemsReceptionReply> Receive(int userId);
}