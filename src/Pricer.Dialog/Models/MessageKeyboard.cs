using System.Collections.Generic;
using System.Linq;
using Pricer.Dialog.Models.Abstract;

namespace Pricer.Dialog.Models;

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