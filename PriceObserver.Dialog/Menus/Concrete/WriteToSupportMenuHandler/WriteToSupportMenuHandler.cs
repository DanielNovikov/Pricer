using System.Threading.Tasks;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Dialog.Common.Abstract;
using PriceObserver.Dialog.Input.Models;
using PriceObserver.Dialog.Menus.Abstract;
using PriceObserver.Dialog.Menus.Models;

namespace PriceObserver.Dialog.Menus.Concrete.WriteToSupportMenuHandler
{
    public class WriteToSupportMenuHandler : IMenuInputHandler
    {
        private readonly IUserActionLogger _userActionLogger;

        public WriteToSupportMenuHandler(IUserActionLogger userActionLogger)
        {
            _userActionLogger = userActionLogger;
        }

        public MenuType Type => MenuType.Support;
        
        public Task<MenuInputHandlingServiceResult> Handle(MessageDto message)
        {
            _userActionLogger.LogWriteToSupport(message.User, message.Text);

            const string responseMessage = "Спасибо за Ваше сообщение, мы с Вами скоро свяжемся! 🏃";
            var result = MenuInputHandlingServiceResult.Success(responseMessage); 
            
            return Task.FromResult(result);
        }
    }
}