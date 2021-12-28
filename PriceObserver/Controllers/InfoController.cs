using Microsoft.AspNetCore.Mvc;

namespace PriceObserver.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InfoController : ControllerBase
{
    [HttpHead]
    public IActionResult Head()
    {
        return Ok();
    }
}