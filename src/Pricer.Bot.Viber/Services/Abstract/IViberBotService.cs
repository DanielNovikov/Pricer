using Pricer.Bot.Abstract;

namespace Pricer.Viber.Services.Abstract;

public interface IViberBotService : IBotService
{
    Task SetWebhook();
}