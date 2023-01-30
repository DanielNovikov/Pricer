using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pricer.Background.Services.Abstract;
using Pricer.Bot.Abstract;
using Pricer.Common.Services.Abstract;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Models;
using Pricer.Data.Persistent.Repositories.Abstract;
using Pricer.Data.Service.Abstract;
using Pricer.Telegram.Abstract;

namespace Pricer.Background.Services.Concrete;

public class ItemRemovalService : IItemRemovalService
{
    private readonly IResourceService _resourceService;
    private readonly IItemParseResultService _parseResultService;
    private readonly ILogger<ItemRemovalService> _logger;
    private readonly IPartnerUrlBuilder _partnerUrlBuilder;
    private readonly IUserRepository _userRepository;
    private readonly IUserLanguage _userLanguage;
    private readonly IItemService _itemService;
    private readonly IBotService _botService;

    private const int CountOfFailedToRemove = 20;
    
    public ItemRemovalService(
        IResourceService resourceService, 
        IItemParseResultService parseResultService, 
        ILogger<ItemRemovalService> logger, 
        IPartnerUrlBuilder partnerUrlBuilder, 
        IUserRepository userRepository, 
        IUserLanguage userLanguage,
        IItemService itemService, 
        IBotService botService)
    {
        _resourceService = resourceService;
        _parseResultService = parseResultService;
        _logger = logger;
        _partnerUrlBuilder = partnerUrlBuilder;
        _userRepository = userRepository;
        _userLanguage = userLanguage;
        _itemService = itemService;
        _botService = botService;
    }

    public async Task Remove(Item item, ResourceKey error)
    {
        var lastErrorsCount = await _parseResultService.GetLastErrorsCount(item.Id);

        if (lastErrorsCount < CountOfFailedToRemove)
        {
            _logger.LogInformation(
                "Item with url {0} is about to be removed. Errors count: {1}", item.Url, lastErrorsCount);
            
            await _parseResultService.CreateFailed(item);
            return;
        }

        var user = await _userRepository.GetById(item.UserId);
        _userLanguage.Set(user.SelectedLanguageKey);
        
        var errorReason = _resourceService.Get(error);
        var partnerUrl = _partnerUrlBuilder.Build(item.Url);
        
        var itemDeletedMessage = _resourceService.Get(
            ResourceKey.Background_ItemDeleted,
            partnerUrl, item.Title, errorReason);

        await _botService.SendText(user.BotKey, user.ExternalId, itemDeletedMessage);
        await _itemService.Delete(item);
        
        _logger.LogInformation(
            @"Item {0} deleted
Reason: {1}
Link: {2}", 
            item.Title, errorReason, item.Url);
    }
}