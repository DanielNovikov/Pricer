using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Pricer.Data.InMemory.Repositories.Abstract;
using Pricer.Dialog.Menus.Abstract;
using Pricer.Dialog.Models;
using Pricer.Dialog.Models.Abstract;
using Pricer.Dialog.Services.Abstract;

namespace Pricer.Dialog.Menus.Concrete;

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

    public async Task<IReplyResult> Handle(MessageModel message)
    {
        var menuKey = message.User.MenuKey;
        var menu = _menuRepository.GetByKey(menuKey);
            
        if (!menu.CanExpectInput)
            return _wrongCommandHandler.Handle(message);

        var handler = _handlers.Single(x => x.Key == menuKey);

        return await handler.Handle(message);
    }
}