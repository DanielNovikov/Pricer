using System.Threading.Tasks;
using PriceObserver.Dialog.Input.Models;

namespace PriceObserver.Dialog.Input.Abstract
{
    public interface IInputHandler
    {
        Task<InputHandlingServiceResult> Handle(UpdateDto update);
    }
}