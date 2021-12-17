using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Dialog.Common.Abstract;
using PriceObserver.Dialog.Input.Models;
using PriceObserver.Dialog.Menus.Abstract;
using PriceObserver.Dialog.Menus.Models;

namespace PriceObserver.Dialog.Menus.Concrete
{
    public class MenuInputHandlerService : IMenuInputHandlerService
    {
        private readonly IReadOnlyList<IMenuInputHandler> _handlers;
        private readonly IUserActionLogger _userActionLogger;
        private readonly IMenuRepository _menuRepository;

        public MenuInputHandlerService(
            IEnumerable<IMenuInputHandler> handlers,
            IUserActionLogger userActionLogger, 
            IMenuRepository menuRepository)
        {
            _handlers = handlers.ToImmutableList();
            _userActionLogger = userActionLogger;
            _menuRepository = menuRepository;
        }

        public async Task<MenuInputHandlingServiceResult> Handle(MessageDto message)
        {
            var menuKey = message.User.MenuKey;
            var menu = _menuRepository.GetByKey(menuKey);
            
            if (!menu.CanExpectInput)
            {
                _userActionLogger.LogWrongCommand(message.User, message.Text);
                return MenuInputHandlingServiceResult.Fail(ResourceKey.Dialog_IncorrectCommand);
            }

            var handler = _handlers.Single(x => x.Type == menuKey);

            return await handler.Handle(message);
        }
    }
}