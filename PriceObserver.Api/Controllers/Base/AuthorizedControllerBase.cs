using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PriceObserver.Api.Controllers.Base;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public abstract class AuthorizedControllerBase : ControllerBase
{
    protected long GetUserId()
    {
        var userIdClaim = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier);
        return long.Parse(userIdClaim.Value);
    }
}