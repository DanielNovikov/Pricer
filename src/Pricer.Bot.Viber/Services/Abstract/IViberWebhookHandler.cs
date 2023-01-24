using Pricer.Viber.Models;

namespace Pricer.Viber.Services.Abstract;

public interface IViberWebhookHandler
{
    Task Handle(ViberWebhookRequest request);
}