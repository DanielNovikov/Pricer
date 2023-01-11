namespace Pricer.Web.Shared.Grpc.HandlerServices;

public interface IItemDeletionHandlerService
{
    Task Delete(int itemId, int userId);
}