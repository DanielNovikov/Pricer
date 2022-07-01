using System.Threading.Tasks;
using PriceObserver.Dialog.Models;

namespace PriceObserver.Dialog.Services.Abstract;

public interface IInputHandler
{
    Task<InputHandlingServiceResult> Handle(UpdateServiceModel update);
}