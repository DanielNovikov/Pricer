using Telegram.Bot.Types;

namespace Pricer.Bot.Telegram.Abstract;

public interface IUpdateHandler
{
    Task Handle(Update update);
}