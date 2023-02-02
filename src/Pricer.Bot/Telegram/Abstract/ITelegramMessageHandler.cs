using Pricer.Dialog.Models;

namespace Pricer.Bot.Telegram.Abstract;

public interface ITelegramMessageHandler
{
    Task Handle(MessageHandlingModel messageHandlingModel);
}