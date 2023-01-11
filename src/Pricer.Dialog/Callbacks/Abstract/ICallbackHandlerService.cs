using System.Threading.Tasks;
using Pricer.Dialog.Models;

namespace Pricer.Dialog.Callbacks.Abstract;

public interface ICallbackHandlerService
{
	Task<CallbackHandlingResult> Handle(CallbackHandlingModel callbackHandlingModel);
}