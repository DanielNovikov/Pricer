using Pricer.Dialog.Models;

namespace Pricer.Viber.Services.Abstract;

public interface IViberMessageHandler
{
    Task Handle(MessageHandlingModel messageHandlingModel);
}