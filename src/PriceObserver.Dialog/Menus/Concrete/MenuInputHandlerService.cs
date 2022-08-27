using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Dialog.Menus.Abstract;
using PriceObserver.Dialog.Models;
using PriceObserver.Dialog.Services.Abstract;

namespace PriceObserver.Dialog.Menus.Concrete;

public class MenuInputHandlerService : IMenuInputHandlerService
{
    private readonly IReadOnlyList<IMenuInputHandler> _handlers;
    private readonly IMenuRepository _menuRepository;
    private readonly IWrongCommandHandler _wrongCommandHandler;

    public MenuInputHandlerService(
        IEnumerable<IMenuInputHandler> handlers,
        IMenuRepository menuRepository,
        IWrongCommandHandler wrongCommandHandler)
    {
        _handlers = handlers.ToImmutableList();
        _menuRepository = menuRepository;
        _wrongCommandHandler = wrongCommandHandler;
    }

    public async Task<MenuInputHandlingServiceResult> Handle(MessageModel message)
    {
        var menuKey = message.User.MenuKey;
        var menu = _menuRepository.GetByKey(menuKey);
            
        if (!menu.CanExpectInput)
        {
            var replyResult = _wrongCommandHandler.Handle(message);
            return MenuInputHandlingServiceResult.Success(replyResult);
        }

        var handler = _handlers.Single(x => x.Key == menuKey);

        return await handler.Handle(message);
    }
}