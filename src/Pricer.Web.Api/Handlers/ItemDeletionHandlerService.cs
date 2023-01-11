using PriceObserver.Data.Persistent.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Web.Shared.Grpc.HandlerServices;

namespace Pricer.Web.Api.Handlers;

public class ItemDeletionHandlerService : IItemDeletionHandlerService
{
    private readonly IItemRepository _itemRepository;
    private readonly IItemService _itemService;

    public ItemDeletionHandlerService(
        IItemRepository itemRepository,
        IItemService itemService)
    {
        _itemRepository = itemRepository;
        _itemService = itemService;
    }

    public async Task Delete(int itemId, int userId)
    {
        var item = await _itemRepository.GetById(itemId);

        if (item == null)
            throw new InvalidOperationException($"Item with {itemId} could not be found");

        if (item.UserId != userId)
            throw new InvalidOperationException($"Item with id {itemId} belongs to another user");

        await _itemService.Delete(item);
    }
}