namespace PriceObserver.Dialog.Models;

public record CallbackResult(
	string MessageText,
	MessageKeyboard? MessageKeyboard, 
	MenuKeyboard? MenuKeyboard)
{
	public CallbackResult(string messageText)
		: this(messageText, null, null)
	{
	}

	public CallbackResult(string messageText, MessageKeyboard messageKeyboard)
		: this(messageText, messageKeyboard, null)
	{
	}

	public CallbackResult(string messageText, MenuKeyboard menuKeyboard) 
		: this(messageText, null, menuKeyboard)
	{
	}
}