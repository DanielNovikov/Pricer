using System.Collections.Generic;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Models;

namespace Pricer.Dialog.Models;

public record CallbackModel(
	CallbackKey Key,
	IList<object> Parameters,
	User User);