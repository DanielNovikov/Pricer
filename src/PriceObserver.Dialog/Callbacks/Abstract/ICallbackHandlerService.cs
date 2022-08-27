using PriceObserver.Dialog.Models;
using System.Threading.Tasks;

namespace PriceObserver.Dialog.Callbacks.Abstract;

public interface ICallbackHandlerService
{
	Task<CallbackHandlingResult> Handle(CallbackHandlingModel callbackHandlingModel);
}