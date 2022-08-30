using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Dialog.Models;
using PriceObserver.Dialog.Services.Abstract;
using System.Text.Json;

namespace PriceObserver.Dialog.Services.Concrete;

public class CallbackDataBuilder : ICallbackDataBuilder
{
	public string BuildJson(CallbackKey key, params object[] parameters)
	{
		var callbackData = new CallbackData(key, parameters);
		return JsonSerializer.Serialize(callbackData);
	}
}