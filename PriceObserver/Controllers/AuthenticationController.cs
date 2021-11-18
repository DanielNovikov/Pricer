using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PriceObserver.Authentication.Abstract;
using PriceObserver.Authentication.Models;

namespace PriceObserver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(
            IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("{token:guid}")]
        public async Task<IActionResult> Authenticate(Guid token)
        {
            var result = await _authenticationService.Authenticate(token);

            if (!result.IsSuccess)
            {
                switch (result.Error)
                {
                    case AuthenticationErrorStatus.TokenNotFound:
                        return BadRequest();
                    case AuthenticationErrorStatus.TokenExpired:
                        return Unauthorized();
                }
            }

            return Ok(result.Result);
        }
    }
}