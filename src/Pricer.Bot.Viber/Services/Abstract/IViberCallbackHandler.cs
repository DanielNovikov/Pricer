using Pricer.Dialog.Models;

namespace Pricer.Viber.Services.Abstract;

public interface IViberCallbackHandler
{
    Task Handle(CallbackHandlingModel callbackHandlingModel);
}