using Telegram.Bot.Types;

namespace Pricer.Dialog.Telegram.Services.Abstract;

public interface IUpdateHandler
{
    Task Handle(Update update);
}