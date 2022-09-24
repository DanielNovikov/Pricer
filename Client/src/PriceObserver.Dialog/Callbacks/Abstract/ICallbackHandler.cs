using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Dialog.Models;
using System.Threading.Tasks;

namespace PriceObserver.Dialog.Callbacks.Abstract;

public interface ICallbackHandler
{
	public CallbackKey Key { get; }
        
	public Task<CallbackHandlingResult> Handle(CallbackModel callback);
}