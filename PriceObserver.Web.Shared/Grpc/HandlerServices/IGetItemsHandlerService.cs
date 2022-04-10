namespace PriceObserver.Web.Shared.Grpc.HandlerServices;

public interface IGetItemsHandlerService
{
    Task<GetItemsReply> Handle(long userId);
}