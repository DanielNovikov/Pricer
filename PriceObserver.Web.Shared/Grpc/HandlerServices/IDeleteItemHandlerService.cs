namespace PriceObserver.Web.Shared.Grpc.HandlerServices;

public interface IDeleteItemHandlerService
{
    Task Handle(int itemId, long userId);
}