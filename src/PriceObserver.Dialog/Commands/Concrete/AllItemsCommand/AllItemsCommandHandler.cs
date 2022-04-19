using System;
using System.Linq;
using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Data.Persistent.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Commands.Abstract;
using PriceObserver.Dialog.Commands.Models;
using PriceObserver.Dialog.Services.Abstract;
using PriceObserver.Dialog.Services.Models;

namespace PriceObserver.Dialog.Commands.Concrete.AllItemsCommand;

public class AllItemsCommandHandler : ICommandHandler
{
    private readonly IItemRepository _itemRepository;
    private readonly IUserActionLogger _userActionLogger;
    private readonly IResourceService _resourceService;
    private readonly IShopRepository _shopRepository;
    private readonly IWebsiteLoginUrlBuilder _websiteLoginUrlBuilder;

    private const int MaximumOfItemsInMessage = 10;
    
    public AllItemsCommandHandler(
        IItemRepository itemRepository,
        IUserActionLogger userActionLogger, 
        IResourceService resourceService, 
        IShopRepository shopRepository, 
        IWebsiteLoginUrlBuilder websiteLoginUrlBuilder)
    {
        _itemRepository = itemRepository;
        _userActionLogger = userActionLogger;
        _resourceService = resourceService;
        _shopRepository = shopRepository;
        _websiteLoginUrlBuilder = websiteLoginUrlBuilder;
    }

    public CommandKey Type => CommandKey.AllItems; 
        
    public async Task<CommandHandlingServiceResult> Handle(User user)
    {
        _userActionLogger.LogAllItemsCalled(user);
            
        var items = await _itemRepository.GetByUserIdWithLimit(user.Id, MaximumOfItemsInMessage);

        if (!items.Any())
            return CommandHandlingServiceResult.Fail(ResourceKey.Dialog_EmptyCart);

        var shops = _shopRepository.GetAll();

        var itemsInfo = items
            .Join(
                shops,
                x => x.ShopKey,
                x => x.Key,
                (i, s) => new
                {
                    Title = i.Title,
                    Url = i.Url,
                    Price = i.Price,
                    CurrencyTitle = _resourceService.Get(s.Currency.Title)
                });
        
        var message = itemsInfo
            .Select((x, i) => 
                _resourceService.Get(ResourceKey.Dialog_ItemInfo, i + 1, x.Title, x.Price, x.CurrencyTitle, x.Url))
            .Aggregate((x, y) => 
                $"{x}{Environment.NewLine}{Environment.NewLine}{y}");

        if (items.Count == MaximumOfItemsInMessage)
        {
            var loginUrl = await _websiteLoginUrlBuilder.Build(user.Id);
            var maximumExceededMessage = _resourceService.Get(ResourceKey.Dialog_MaximumOfItemsExceeded, loginUrl);

            message += $"{Environment.NewLine}{Environment.NewLine}{maximumExceededMessage}";
        }

        var replyResult = ReplyResult.Reply(message);
        return CommandHandlingServiceResult.Success(replyResult);
    }
}