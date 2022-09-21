using System.Collections.Generic;
using PriceObserver.Dialog.Models.Abstract;

namespace PriceObserver.Dialog.Models;

public record MenuKeyboard(List<List<MenuKeyboardButton>> Buttons) : IReplyKeyboard;