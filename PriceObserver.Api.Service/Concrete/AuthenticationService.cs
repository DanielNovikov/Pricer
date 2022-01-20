﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using PriceObserver.Api.Services.Abstract;
using PriceObserver.Api.Services.Models.Response;
using PriceObserver.Api.Services.Models.Service;
using PriceObserver.Api.Services.Options;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;

namespace PriceObserver.Api.Services.Concrete;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserTokenRepository _userTokenRepository;
    private readonly IUserTokenService _userTokenService;
        
    public AuthenticationService(
        IUserTokenRepository userTokenRepository, 
        IUserTokenService userTokenService)
    {
        _userTokenRepository = userTokenRepository;
        _userTokenService = userTokenService;
    }

    public async Task<AuthenticationServiceResult> Authenticate(Guid token)
    {
        var userToken = await _userTokenRepository.GetByToken(token);

        if (userToken is null)
            return AuthenticationServiceResult.Fail(AuthenticationErrorStatus.TokenNotFound);

        if (userToken.Expired)
            return AuthenticationServiceResult.Fail(AuthenticationErrorStatus.TokenExpired);

        await _userTokenService.Expire(userToken);
            
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userToken.UserId.ToString())
        };

        var securityToken = PrivateKey.GetSymmetricSecurityKey();
        var credentials = new SigningCredentials(securityToken, SecurityAlgorithms.HmacSha256Signature);
            
        var jwtToken = new JwtSecurityToken(
            claims: claims,
            signingCredentials: credentials);

        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        var responseModel = new AuthenticationResponseModel(accessToken);

        return AuthenticationServiceResult.Success(responseModel);
    }
}