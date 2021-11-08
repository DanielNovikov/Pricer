using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PriceObserver.Model.Data.Enums;
using PriceObserver.Model.Telegram.Menu;
using PriceObserver.Telegram.Dialog.Common.Extensions;
using PriceObserver.Telegram.Dialog.Menus.Abstract;
using Telegram.Bot.Types;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Telegram.Dialog.Menus.Concrete.WriteToSupportMenuHandler
{
    public class WriteToSupportMenuHandler : IMenuInputHandler
    {
        private readonly ILogger _logger;

        public WriteToSupportMenuHandler(ILogger<WriteToSupportMenuHandler> logger)
        {
            _logger = logger;
        }
        
        public MenuType Type => MenuType.Support;
        
        public async Task<MenuInputHandlingServiceResult> Handle(Update update, User user)
        {
            var message = update.GetMessageText();

            var log = $@"Сообщение: {message}
Логин: @{user.Username} - {user.FirstName} {user.LastName}";
            
            _logger.LogInformation(log);
            
            return MenuInputHandlingServiceResult.Success("Спасибо за Ваше сообщение, мы с вами скоро свяжемся! 🏃");
        }
    }
}