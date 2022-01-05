using System.Threading.Tasks;
using PriceObserver.Background.JobServices.Abstract;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Models;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Telegram.Abstract;

namespace PriceObserver.Background.JobServices.Concrete;

public class ItemRemovalService : IItemRemovalService
{
    private readonly IItemRepository _itemRepository;
    private readonly IResourceService _resourceService;
    private readonly ITelegramBotService _telegramBotService;
    
    public ItemRemovalService(
        IItemRepository itemRepository,
        IResourceService resourceService, 
        ITelegramBotService telegramBotService)
    {
        _itemRepository = itemRepository;
        _resourceService = resourceService;
        _telegramBotService = telegramBotService;
    }

    public async Task Remove(Item item, ResourceKey error)
    {
        await _itemRepository.Delete(item);

        var errorReason = _resourceService.Get(error);
        var itemDeletedMessage = _resourceService.Get(
            ResourceKey.Background_ItemDeleted,
            item.Url.ToString(),
            item.Title,
            errorReason);

        await _telegramBotService.SendMessage(item.UserId, itemDeletedMessage);   
    }
}