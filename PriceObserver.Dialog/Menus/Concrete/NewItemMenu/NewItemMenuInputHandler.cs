using System;
using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Data.Models;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Menus.Abstract;
using PriceObserver.Dialog.Menus.Models;
using PriceObserver.Dialog.Services.Abstract;
using PriceObserver.Dialog.Services.Models;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Dialog.Menus.Concrete.NewItemMenu;

public class NewItemMenuHandler : IMenuInputHandler
{
    private readonly IUrlExtractor _urlExtractor;
    private readonly IUserActionLogger _userActionLogger;
    private readonly IUserItemParser _userItemParser;

    public NewItemMenuHandler(
        IUrlExtractor urlExtractor,
        IUserActionLogger userActionLogger, 
        IUserItemParser userItemParser)
    {
        _urlExtractor = urlExtractor;
        _userActionLogger = userActionLogger;
        _userItemParser = userItemParser;
    }

    public MenuKey Key => MenuKey.NewItem;
        
    public async Task<MenuInputHandlingServiceResult> Handle(MessageServiceModel message)
    {
        var urlExtractionResult = _urlExtractor.Extract(message.Text);

        if (!urlExtractionResult.IsSuccess)
        {
            _userActionLogger.LogWrongUrlPassed(message.User, message.Text, urlExtractionResult.Error);
            return MenuInputHandlingServiceResult.Fail(urlExtractionResult.Error);
        }

        var url = urlExtractionResult.Result;
        var parseResult = await _userItemParser.Parse(message.User, url);

        if (!parseResult.IsSuccess)
            return MenuInputHandlingServiceResult.Fail(parseResult.Error);

        return MenuInputHandlingServiceResult.Success(parseResult.Result);
    }
}