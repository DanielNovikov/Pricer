using Pricer.Bot.Viber.Models;

namespace Pricer.Bot.Viber.Services.Abstract;

public interface IViberWebhookHandler
{
    Task Handle(ViberWebhookRequest request);
}