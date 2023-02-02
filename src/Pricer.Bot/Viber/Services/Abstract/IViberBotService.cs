using Pricer.Bot.Abstract;

namespace Pricer.Bot.Viber.Services.Abstract;

public interface IViberBotService : IBotProviderService
{
    Task SetWebhook();
}