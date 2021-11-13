using System.Threading.Tasks;
using PriceObserver.Model.Telegram.Input;

namespace PriceObserver.Telegram.Dialog.Input.Abstract
{
    public interface IInputHandler
    {
        Task<InputHandlingServiceResult> Handle(UpdateDto update);
    }
}