using Pricer.Dialog.Common.Models.Abstract;

namespace Pricer.Dialog.Common.Models.Callback;

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