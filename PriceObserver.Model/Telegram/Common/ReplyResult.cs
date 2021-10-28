using Telegram.Bot.Types.ReplyMarkups;

namespace PriceObserver.Model.Telegram.Common
{
    public class ReplyResult
    {
        public string Message { get; private set; }
        
        public ReplyKeyboardMarkup MenuKeyboard { get; private set; }

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
        
        public static ReplyResult ReplyWithKeyboard(string message, ReplyKeyboardMarkup menuKeyBoard)
        {
            return new ReplyResult
            {
                Message = message,
                MenuKeyboard = menuKeyBoard
            };
        }
    }
}