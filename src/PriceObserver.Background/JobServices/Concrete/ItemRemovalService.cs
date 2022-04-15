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
    private readonly IItemParseResultService _parseResultService;
    private readonly IItemParseResultRepository _parseResultRepository;
    
    public ItemRemovalService(
        IItemRepository itemRepository,
        IResourceService resourceService, 
        ITelegramBotService telegramBotService, 
        IItemParseResultService parseResultService, 
        IItemParseResultRepository parseResultRepository)
    {
        _itemRepository = itemRepository;
        _resourceService = resourceService;
        _telegramBotService = telegramBotService;
        _parseResultService = parseResultService;
        _parseResultRepository = parseResultRepository;
    }

    public async Task Remove(Item item, ResourceKey error)
    {
        var lastParseResult = await _parseResultRepository.GetLastByItemId(item.Id);

        if (lastParseResult is null || lastParseResult.IsSuccess)
        {
            await _parseResultService.CreateFailed(item);
            return;
        }
        
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