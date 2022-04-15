namespace PriceObserver.Dialog.Services.Models;

public class ReplyResult
{
    public string Message { get; private init; }
        
    public MenuKeyboard MenuKeyboard { get; private init; }

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
        
    public static ReplyResult ReplyWithKeyboard(string message, MenuKeyboard menuKeyboard)
    {
        return new ReplyResult
        {
            Message = message,
            MenuKeyboard = menuKeyboard
        };
    }
}