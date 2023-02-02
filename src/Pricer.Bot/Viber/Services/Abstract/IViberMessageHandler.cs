using Pricer.Dialog.Models;

namespace Pricer.Bot.Viber.Services.Abstract;

public interface IViberMessageHandler
{
    Task Handle(MessageHandlingModel messageHandlingModel);
}