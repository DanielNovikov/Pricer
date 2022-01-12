using System;
using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Data.Models;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Common.Abstract;
using PriceObserver.Dialog.Input.Models;
using PriceObserver.Dialog.Menus.Abstract;
using PriceObserver.Dialog.Menus.Abstract.NewItemMenu;
using PriceObserver.Dialog.Menus.Models;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Dialog.Menus.Concrete.NewItemMenu;

public class NewItemMenuHandler : IMenuInputHandler
{
    private readonly IItemRepository _itemRepository;
    private readonly IUrlExtractor _urlExtractor;
    private readonly IUserActionLogger _userActionLogger;
    private readonly IUserItemParser _userItemParser;

    public NewItemMenuHandler(
        IItemRepository itemRepository,
        IUrlExtractor urlExtractor,
        IUserActionLogger userActionLogger, 
        IUserItemParser userItemParser)
    {
        _itemRepository = itemRepository;
        _urlExtractor = urlExtractor;
        _userActionLogger = userActionLogger;
        _userItemParser = userItemParser;
    }

    public MenuKey Type => MenuKey.NewItem;
        
    public async Task<MenuInputHandlingServiceResult> Handle(MessageDto message)
    {
        var urlExtractionResult = _urlExtractor.Extract(message.Text);

        if (!urlExtractionResult.IsSuccess)
        {
            _userActionLogger.LogWrongUrlPassed(message.User, message.Text, urlExtractionResult.Error);
            return MenuInputHandlingServiceResult.Fail(urlExtractionResult.Error);
        }

        var url = urlExtractionResult.Result;

        var itemExists = await _itemRepository.ExistsForUserByUrl(message.User.Id, url);
        if (itemExists)
        {
            _userActionLogger.LogDuplicateItem(message.User, url);
            return MenuInputHandlingServiceResult.Fail(ResourceKey.Dialog_DuplicateItem);
        }

        var parseResult = await _userItemParser.Parse(message.User, url);

        if (!parseResult.IsSuccess)
            return MenuInputHandlingServiceResult.Fail(parseResult.Error);

        return MenuInputHandlingServiceResult.Success(parseResult.Result);
    }
}