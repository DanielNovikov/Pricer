using Pricer.Dialog.Common.Models;

namespace Pricer.Dialog.Callback.Models;

public record CallbackHandlingModel(string Data, int MessageId, UserModel User);