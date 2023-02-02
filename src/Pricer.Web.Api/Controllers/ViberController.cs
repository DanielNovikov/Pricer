using Microsoft.AspNetCore.Mvc;
using Pricer.Bot.Viber.Models;
using Pricer.Bot.Viber.Models.Enums;
using Pricer.Bot.Viber.Services.Abstract;

namespace Pricer.Web.Api.Controllers;

[Route("api/viber")]
public class ViberController : Controller
{
    private readonly IViberWebhookHandler _webhookHandler;

    public ViberController(IViberWebhookHandler webhookHandler)
    {
        _webhookHandler = webhookHandler;
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> Webhook([FromBody] ViberWebhookRequest request)
    {
        if (request.Event == EventType.Webhook)
            return Ok();

        await _webhookHandler.Handle(request);

        return Ok();
    }
}