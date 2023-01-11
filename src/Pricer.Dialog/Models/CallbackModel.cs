using System.Collections.Generic;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models;

namespace Pricer.Dialog.Models;

public record CallbackModel(
	CallbackKey Key,
	IList<object> Parameters,
	User User);