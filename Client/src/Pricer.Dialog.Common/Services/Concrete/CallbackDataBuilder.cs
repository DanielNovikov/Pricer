using System.Text.Json;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Dialog.Common.Models.Callback;
using Pricer.Dialog.Common.Services.Abstract;

namespace Pricer.Dialog.Common.Services.Concrete;

public class CallbackDataBuilder : ICallbackDataBuilder
{
	public string BuildJson(CallbackKey key, params object[] parameters)
	{
		var callbackData = new CallbackData(key, parameters);
		return JsonSerializer.Serialize(callbackData);
	}
}