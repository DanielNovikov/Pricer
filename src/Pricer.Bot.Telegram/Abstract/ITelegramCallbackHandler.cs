using System.Threading.Tasks;
using Pricer.Bot.Abstract;
using Pricer.Dialog.Models;

namespace Pricer.Telegram.Abstract;

public interface ITelegramCallbackHandler
{
    Task Handle(CallbackHandlingModel callbackHandlingModel);
}