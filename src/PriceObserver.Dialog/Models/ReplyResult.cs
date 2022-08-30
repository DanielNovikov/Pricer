namespace PriceObserver.Dialog.Models;

public class ReplyResult
{
    public string Message { get; private init; }
        
    public MenuKeyboard? MenuKeyboard { get; private init; }
    
    public MessageKeyboard? MessageKeyboard { get; private init; }

    private ReplyResult()
    {
    }

    public static ReplyResult Reply(string message)
    {
        return new ReplyResult
        {
            Message = message
        };
    }
        
    public static ReplyResult ReplyWithMenuKeyboard(string message, MenuKeyboard menuKeyboard)
    {
        return new ReplyResult
        {
            Message = message,
            MenuKeyboard = menuKeyboard
        };
    }

    public static ReplyResult ReplyWithMessageKeyboard(string message, MessageKeyboard messageKeyboard)
    {
        return new ReplyResult
        {
            Message = message,
            MessageKeyboard = messageKeyboard
        };
    }
}