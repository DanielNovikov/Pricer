using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Models;

namespace Pricer.Dialog.Callback.Models;

public record CallbackModel(
	CallbackKey Key,
	IList<object> Parameters,
	User User);