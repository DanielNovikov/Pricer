using System.Threading.Tasks;
using PriceObserver.Model.Dialog.Input;

namespace PriceObserver.Dialog.Input.Abstract
{
    public interface IInputHandler
    {
        Task<InputHandlingServiceResult> Handle(UpdateDto update);
    }
}