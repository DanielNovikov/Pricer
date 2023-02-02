using Pricer.Dialog.Models;

namespace Pricer.Bot.Telegram.Abstract;

public interface ITelegramCallbackHandler
{
    Task Handle(CallbackHandlingModel callbackHandlingModel);
}