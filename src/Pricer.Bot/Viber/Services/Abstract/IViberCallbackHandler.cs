using Pricer.Dialog.Models;

namespace Pricer.Bot.Viber.Services.Abstract;

public interface IViberCallbackHandler
{
    Task Handle(CallbackHandlingModel callbackHandlingModel);
}