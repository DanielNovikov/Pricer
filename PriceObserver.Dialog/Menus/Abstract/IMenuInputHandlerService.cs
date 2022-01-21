using System.Threading.Tasks;
using PriceObserver.Dialog.Menus.Models;
using PriceObserver.Dialog.Services.Models;

namespace PriceObserver.Dialog.Menus.Abstract;

public interface IMenuInputHandlerService
{
    Task<MenuInputHandlingServiceResult> Handle(MessageServiceModel message);
}