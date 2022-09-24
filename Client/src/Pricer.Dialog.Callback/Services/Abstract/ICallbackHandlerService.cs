using Pricer.Dialog.Callback.Models;

namespace Pricer.Dialog.Callback.Services.Abstract;

public interface ICallbackHandlerService
{
	Task<CallbackHandlingResult> Handle(CallbackHandlingModel callbackHandlingModel);
}