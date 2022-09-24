using Pricer.Dialog.Common.Models.Abstract;

namespace Pricer.Dialog.Common.Models.Input;

public record MenuKeyboard(List<List<MenuKeyboardButton>> Buttons) : IReplyKeyboard;