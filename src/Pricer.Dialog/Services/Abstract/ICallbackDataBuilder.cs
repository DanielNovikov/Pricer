using PriceObserver.Data.InMemory.Models.Enums;

namespace Pricer.Dialog.Services.Abstract;

public interface ICallbackDataBuilder
{
	string BuildJson(CallbackKey key, params object[] parameters);
}