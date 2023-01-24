using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pricer.Viber.Models;
using Pricer.Viber.Models.Enums;
using Pricer.Viber.Models.Message;
using Pricer.Viber.Services.Abstract;

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