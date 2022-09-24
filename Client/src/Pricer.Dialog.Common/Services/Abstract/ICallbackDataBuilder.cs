using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Dialog.Common.Services.Abstract;

public interface ICallbackDataBuilder
{
	string BuildJson(CallbackKey key, params object[] parameters);
}