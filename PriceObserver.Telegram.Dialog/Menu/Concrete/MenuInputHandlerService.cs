using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using PriceObserver.Model.Telegram.Menu;
using PriceObserver.Telegram.Dialog.Menu.Abstract;
using Telegram.Bot.Types;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Telegram.Dialog.Menu.Concrete
{
    public class MenuInputHandlerService : IMenuInputHandlerService
    {
        private readonly IReadOnlyList<IMenuInputHandler> _handlers;

        public MenuInputHandlerService(IEnumerable<IMenuInputHandler> handlers)
        {
            _handlers = handlers.ToImmutableList();
        }

        public async Task<MenuInputHandlingServiceResult> Handle(Update update, User user)
        {
            if (!user.Menu.CanExpectInput)
                return MenuInputHandlingServiceResult.Fail("Неверная комманда");
            
            var handler = _handlers.Single(x => x.Type == user.Menu.Type);

            return await handler.Handle(update, user);
        }
    }
}