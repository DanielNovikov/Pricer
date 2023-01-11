using System.Collections.Generic;
using Pricer.Dialog.Models.Abstract;

namespace Pricer.Dialog.Models;

public record MenuKeyboard(List<List<MenuKeyboardButton>> Buttons) : IReplyKeyboard;