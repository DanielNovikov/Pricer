﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PriceObserver.Api.Services.Abstract;
using PriceObserver.Api.Services.Models;
using PriceObserver.Api.Services.Models.Service;

namespace PriceObserver.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
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