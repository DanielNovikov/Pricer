using Telegram.Bot.Types;

namespace PriceObserver.Telegram.Dialog.Common.Extensions
{
    public static class UpdateExtensions
    {
        public static long GetUserId(this Update update)
        {
            return update.Message.Chat.Id;
        }

        public static string GetMessageText(this Update update)
        {
            return update.Message.Text;
        }
    }
}