using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Menus.Abstract;
using PriceObserver.Dialog.Menus.Models;
using PriceObserver.Dialog.Services.Abstract;
using PriceObserver.Dialog.Services.Models;

namespace PriceObserver.Dialog.Menus.Concrete.WriteToSupportMenu;

public class WriteToSupportMenuInputHandler : IMenuInputHandler
{
    private readonly IUserActionLogger _userActionLogger;
    private readonly IResourceService _resourceService;

    public WriteToSupportMenuInputHandler(
        IUserActionLogger userActionLogger,
        IResourceService resourceService)
    {
        _userActionLogger = userActionLogger;
        _resourceService = resourceService;
    }

    public MenuKey Key => MenuKey.Support;
        
    public Task<MenuInputHandlingServiceResult> Handle(MessageServiceModel message)
    {
        _userActionLogger.LogWriteToSupport(message.User, message.Text);

        var responseMessage = _resourceService.Get(ResourceKey.Dialog_SupportReply);
        var result = MenuInputHandlingServiceResult.Success(responseMessage); 
            
        return Task.FromResult(result);
    }
}