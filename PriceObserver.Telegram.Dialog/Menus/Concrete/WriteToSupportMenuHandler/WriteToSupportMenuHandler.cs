using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PriceObserver.Model.Data.Enums;
using PriceObserver.Model.Telegram.Input;
using PriceObserver.Model.Telegram.Menu;
using PriceObserver.Telegram.Dialog.Menus.Abstract;

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
        
        public Task<MenuInputHandlingServiceResult> Handle(MessageDto message)
        {
            var user = message.User;
            var log = $@"Сообщение: {message.Text}
Логин: @{user.Username} - {user.FirstName} {user.LastName}";
            
            _logger.LogInformation(log);

            var result =
                MenuInputHandlingServiceResult.Success("Спасибо за Ваше сообщение, мы с вами скоро свяжемся! 🏃"); 
            
            return Task.FromResult(result);
        }
    }
}