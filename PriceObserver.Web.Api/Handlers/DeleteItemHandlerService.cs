using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Web.Shared.Grpc.HandlerServices;

namespace PriceObserver.Web.Api.Handlers;

public class DeleteItemHandlerService : IDeleteItemHandlerService
{
    private readonly IItemRepository _itemRepository;

    public DeleteItemHandlerService(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task Handle(int itemId, long userId)
    {
        var item = await _itemRepository.GetById(itemId);

        if (item == null)
            throw new InvalidOperationException($"Item with {itemId} could not be found");

        if (item.UserId != userId)
            throw new InvalidOperationException($"Item with id {itemId} belongs to another user");

        await _itemRepository.Delete(item);
    }
}