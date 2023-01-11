using System.Text.Json;
using PriceObserver.Data.InMemory.Models.Enums;
using Pricer.Dialog.Models;
using Pricer.Dialog.Services.Abstract;

namespace Pricer.Dialog.Services.Concrete;

public class CallbackDataBuilder : ICallbackDataBuilder
{
	public string BuildJson(CallbackKey key, params object[] parameters)
	{
		var callbackData = new CallbackData(key, parameters);
		return JsonSerializer.Serialize(callbackData);
	}
}