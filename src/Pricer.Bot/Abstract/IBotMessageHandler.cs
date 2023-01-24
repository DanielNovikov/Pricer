using Pricer.Dialog.Models;

namespace Pricer.Bot.Abstract;

public interface IBotMessageHandler
{
    Task Handle(MessageHandlingModel messageHandlingModel, IBotService botService);
}