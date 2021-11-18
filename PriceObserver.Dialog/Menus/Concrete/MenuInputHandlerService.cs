using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using PriceObserver.Dialog.Menus.Abstract;
using PriceObserver.Model.Dialog.Input;
using PriceObserver.Model.Dialog.Menu;

namespace PriceObserver.Dialog.Menus.Concrete
{
    public class MenuInputHandlerService : IMenuInputHandlerService
    {
        private readonly IReadOnlyList<IMenuInputHandler> _handlers;

        public MenuInputHandlerService(IEnumerable<IMenuInputHandler> handlers)
        {
            _handlers = handlers.ToImmutableList();
        }

        public async Task<MenuInputHandlingServiceResult> Handle(MessageDto message)
        {
            var userMenu = message.User.Menu;
            if (!userMenu.CanExpectInput)
                return MenuInputHandlingServiceResult.Fail("Неверная комманда");
            
            var handler = _handlers.Single(x => x.Type == userMenu.Type);

            return await handler.Handle(message);
        }
    }
}