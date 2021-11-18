using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
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

        public MenuInputHandlerService(
            IEnumerable<IMenuInputHandler> handlers,
            IUserActionLogger userActionLogger)
        {
            _handlers = handlers.ToImmutableList();
            _userActionLogger = userActionLogger;
        }

        public async Task<MenuInputHandlingServiceResult> Handle(MessageDto message)
        {
            var userMenu = message.User.Menu;
            if (!userMenu.CanExpectInput)
            {
                _userActionLogger.LogWrongCommand(message.User, message.Text);
                return MenuInputHandlingServiceResult.Fail("Неверная комманда");
            }

            var handler = _handlers.Single(x => x.Type == userMenu.Type);

            return await handler.Handle(message);
        }
    }
}