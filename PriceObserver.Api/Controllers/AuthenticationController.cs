using Microsoft.AspNetCore.Mvc;
using PriceObserver.Api.Models.Service;
using PriceObserver.Api.Services.Abstract;

namespace PriceObserver.Api.Controllers;

[Route("api")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("authorize/{token:guid}")]
    public async Task<IActionResult> Authenticate(Guid token)
    {
        var result = await _authenticationService.Authenticate(token);

        if (!result.IsSuccess)
        {
            return result.Error switch
            {
                AuthenticationErrorStatus.TokenNotFound => BadRequest(),
                AuthenticationErrorStatus.TokenExpired => Unauthorized(),
                _ => throw new ArgumentOutOfRangeException(nameof(result.Error))
            };
        }

        return Ok(result.Result);
    }
}