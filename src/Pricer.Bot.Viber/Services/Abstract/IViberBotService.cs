using Pricer.Bot.Abstract;

namespace Pricer.Viber.Services.Abstract;

public interface IViberBotService : IBotProviderService
{
    Task SetWebhook();
}