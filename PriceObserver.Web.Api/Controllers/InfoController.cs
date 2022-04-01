using Microsoft.AspNetCore.Mvc;

namespace PriceObserver.Web.Api.Controllers;

[Route("api/info")]
[ApiController]
public class InfoController : ControllerBase
{
    [HttpHead]
    public IActionResult Head()
    {
        return Ok();
    }
}