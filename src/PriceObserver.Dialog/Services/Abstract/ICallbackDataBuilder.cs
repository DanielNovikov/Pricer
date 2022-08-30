using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Dialog.Services.Abstract;

public interface ICallbackDataBuilder
{
	string BuildJson(CallbackKey key, params object[] parameters);
}