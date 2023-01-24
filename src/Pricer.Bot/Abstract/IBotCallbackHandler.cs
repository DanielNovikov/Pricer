using Pricer.Dialog.Models;

namespace Pricer.Bot.Abstract;

public interface IBotCallbackHandler
{
    Task Handle(CallbackHandlingModel callbackHandlingModel, IBotService botService);
}