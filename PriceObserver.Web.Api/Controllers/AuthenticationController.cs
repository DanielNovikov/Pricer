using Microsoft.AspNetCore.Mvc;
using PriceObserver.Web.Shared.Services.Abstract;

namespace PriceObserver.Web.Api.Controllers;

[Route("api/authentication")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("{token:guid}")]
    public async Task<IActionResult> Authenticate(Guid token)
    {
        var result = await _authenticationService.Authenticate(token);

        if (!result.IsSuccess)
            return Unauthorized();

        return Ok(result.Result);
    }
}