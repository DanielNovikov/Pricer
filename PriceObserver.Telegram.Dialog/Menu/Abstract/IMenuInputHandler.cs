using System.Threading.Tasks;
using PriceObserver.Model.Data.Enums;
using PriceObserver.Model.Telegram.Menu;
using Telegram.Bot.Types;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Telegram.Dialog.Menu.Abstract
{
    public interface IMenuInputHandler
    {
        MenuType Type { get; }

        Task<MenuInputHandlingServiceResult> Handle(Update update, User user);
    }
}