using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PriceObserver.Dialog.Menus.Abstract;
using PriceObserver.Model.Data.Enums;
using PriceObserver.Model.Dialog.Input;
using PriceObserver.Model.Dialog.Menu;

namespace PriceObserver.Dialog.Menus.Concrete.WriteToSupportMenuHandler
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