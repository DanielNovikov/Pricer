using System.Threading.Tasks;
using PriceObserver.Model.Telegram.Menu;
using Telegram.Bot.Types;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Telegram.Dialog.Menu.Abstract
{
    public interface IMenuInputHandlerService
    {
        Task<MenuInputHandlingServiceResult> Handle(Update update, User user);
    }
}