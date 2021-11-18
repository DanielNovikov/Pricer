using Telegram.Bot.Types.ReplyMarkups;

namespace PriceObserver.Model.Telegram.Common
{
    public class ReplyResult
    {
        public string Message { get; private set; }
        
        public MenuKeyboard MenuKeyboard { get; private set; }

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
}