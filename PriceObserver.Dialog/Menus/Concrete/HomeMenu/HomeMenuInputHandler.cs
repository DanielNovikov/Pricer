using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Dialog.Menus.Abstract;
using PriceObserver.Dialog.Menus.Models;
using PriceObserver.Dialog.Services.Abstract;
using PriceObserver.Dialog.Services.Models;

namespace PriceObserver.Dialog.Menus.Concrete.HomeMenu;

public class HomeMenuInputHandler : IMenuInputHandler
{
    private readonly IUrlExtractor _urlExtractor;
    private readonly IUserActionLogger _userActionLogger;
    private readonly IUserItemParser _userItemParser;

    public HomeMenuInputHandler(
        IUrlExtractor urlExtractor,
        IUserActionLogger userActionLogger,
        IUserItemParser userItemParser)
    {
        _urlExtractor = urlExtractor;
        _userActionLogger = userActionLogger;
        _userItemParser = userItemParser;
    }

    public MenuKey Key => MenuKey.Home;
    
    public async Task<MenuInputHandlingServiceResult> Handle(MessageServiceModel message)
    {
        var urlExtractionResult = _urlExtractor.Extract(message.Text);

        if (!urlExtractionResult.IsSuccess)
        {
            _userActionLogger.LogWrongCommand(message.User, message.Text);
            return MenuInputHandlingServiceResult.Fail(ResourceKey.Dialog_IncorrectCommand);
        }

        var url = urlExtractionResult.Result;
        var parseResult = await _userItemParser.Parse(message.User, url);

        if (!parseResult.IsSuccess)
            return MenuInputHandlingServiceResult.Fail(parseResult.Error);

        return MenuInputHandlingServiceResult.Success(parseResult.Result);
    }
}