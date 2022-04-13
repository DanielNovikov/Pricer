namespace PriceObserver.Web.Shared.Grpc.HandlerServices;

public interface IItemsReceptionHandlerService
{
    Task<ItemsReceptionReply> Receive(long userId);
}