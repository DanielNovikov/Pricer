﻿using System.Security.Claims;
using Microsoft.Extensions.Logging;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Web.Api.Services.Abstract;
using PriceObserver.Web.Shared.Grpc;
using PriceObserver.Web.Shared.Grpc.HandlerServices;

namespace PriceObserver.Web.Api.Handlers;

public class AuthenticationHandlerService : IAuthenticationHandlerService
{
    private readonly IUserTokenRepository _userTokenRepository;
    private readonly IUserTokenService _userTokenService;
    private readonly ILogger<AuthenticationHandlerService> _logger;
    private readonly IJwtService _jwtService;

    public AuthenticationHandlerService(
        IUserTokenRepository userTokenRepository,
        IUserTokenService userTokenService,
        ILogger<AuthenticationHandlerService> logger, 
        IJwtService jwtService)
    {
        _userTokenRepository = userTokenRepository;
        _userTokenService = userTokenService;
        _logger = logger;
        _jwtService = jwtService;
    }

    public async Task<AuthenticationReply> Authenticate(Guid token)
    {
        var userToken = await _userTokenRepository.GetByToken(token);

        if (userToken is null)
        {
            _logger.LogInformation("User tried to use not existing token '{0}'", token);
            return new AuthenticationReply { IsSuccess = false };
        }

        if (userToken.Expired)
        {
            _logger.LogInformation(
                "User with id '{0}' tried to use expired token '{1}'",
                userToken.UserId,
                userToken.Token);
            
            return new AuthenticationReply { IsSuccess = false };
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userToken.UserId.ToString())
        };
        var accessToken = _jwtService.Create(claims);
        
        await _userTokenService.Expire(userToken);
        _logger.LogInformation("User with id {0} authenticated", userToken.UserId);
        
        return new AuthenticationReply
        {
            IsSuccess = true, 
            AccessToken = accessToken
        };
    }
}