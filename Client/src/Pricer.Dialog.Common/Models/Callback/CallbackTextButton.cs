using Pricer.Dialog.Common.Models.Abstract;

namespace Pricer.Dialog.Common.Models.Callback;

public record CallbackTextButton(string Text, string Data) : IMessageKeyboardButton;