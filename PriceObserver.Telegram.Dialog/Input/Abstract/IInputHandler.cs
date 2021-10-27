using System.Threading.Tasks;
using PriceObserver.Model.Telegram.Input;
using PriceObserver.Model.Telegram.Menu;
using Telegram.Bot.Types;

namespace PriceObserver.Telegram.Dialog.Input.Abstract
{
    public interface IInputHandler
    {
        Task<InputHandlingServiceResult> Handle(Update update);
    }
}