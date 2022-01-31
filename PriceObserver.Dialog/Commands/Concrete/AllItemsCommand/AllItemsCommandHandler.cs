using System;
using System.Linq;
using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Data.Models;
using PriceObserver.Data.Repositories.Abstract;
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
        
    public AllItemsCommandHandler(
        IItemRepository itemRepository,
        IUserActionLogger userActionLogger, 
        IResourceService resourceService, 
        IShopRepository shopRepository)
    {
        _itemRepository = itemRepository;
        _userActionLogger = userActionLogger;
        _resourceService = resourceService;
        _shopRepository = shopRepository;
    }

    public CommandKey Type => CommandKey.AllItems; 
        
    public async Task<CommandHandlingServiceResult> Handle(User user)
    {
        _userActionLogger.LogAllItemsCalled(user);
            
        var items = await _itemRepository.GetByUserId(user.Id);

        if (!items.Any())
            return CommandHandlingServiceResult.Fail(ResourceKey.Dialog_EmptyCart);

        var shops = _shopRepository.GetAll();
        
        var message = items
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
                })
            .Select((x, i) => 
                _resourceService.Get(ResourceKey.Dialog_ItemInfo, i + 1, x.Title, x.Price, x.CurrencyTitle, x.Url))
            .Aggregate((x, y) => $"{x}{Environment.NewLine}{Environment.NewLine}{y}");

        var replyResult = ReplyResult.Reply(message);

        return CommandHandlingServiceResult.Success(replyResult);
    }
}