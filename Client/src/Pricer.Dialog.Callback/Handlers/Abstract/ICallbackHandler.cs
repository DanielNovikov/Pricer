using Pricer.Data.InMemory.Models.Enums;
using Pricer.Dialog.Callback.Models;

namespace Pricer.Dialog.Callback.Handlers.Abstract;

public interface ICallbackHandler
{
	public CallbackKey Key { get; }
        
	public Task<CallbackHandlingResult> Handle(CallbackModel callback);
}