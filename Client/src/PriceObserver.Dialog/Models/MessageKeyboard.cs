using System.Collections.Generic;
using System.Linq;
using PriceObserver.Dialog.Models.Abstract;

namespace PriceObserver.Dialog.Models;

public record MessageKeyboard(List<List<IMessageKeyboardButton>> Buttons) : IReplyKeyboard
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