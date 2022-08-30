using System.Collections.Generic;
using System.Linq;

namespace PriceObserver.Dialog.Models;

public record MessageKeyboard(List<List<IMessageKeyboardButton>> Buttons)
{
	public MessageKeyboard(List<IMessageKeyboardButton> buttons) 
		: this(new List<List<IMessageKeyboardButton>> { buttons })
	{
	}
	
	public MessageKeyboard(params IMessageKeyboardButton[] buttons) 
		: this(new List<List<IMessageKeyboardButton>> { buttons.ToList() })
	{
	}
}