using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models;
using System.Collections.Generic;

namespace PriceObserver.Dialog.Models;

public record CallbackModel(
	CallbackKey Key,
	IList<object> Parameters,
	User User);