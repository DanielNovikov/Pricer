using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PriceObserver.Api.Extensions;
using PriceObserver.Api.Models.Response;
using PriceObserver.Api.Models.Service;
using PriceObserver.Api.Services.Abstract;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;

namespace PriceObserver.Api.Services.Concrete;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserTokenRepository _userTokenRepository;
    private readonly IUserTokenService _userTokenService;
    private readonly IConfiguration _configuration;
    private readonly ILogger _logger;

    public AuthenticationService(
        IUserTokenRepository userTokenRepository,
        IUserTokenService userTokenService,
        IConfiguration configuration,
        ILogger<AuthenticationService> logger)
    {
        _userTokenRepository = userTokenRepository;
        _userTokenService = userTokenService;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<AuthenticationServiceResult> Authenticate(Guid token)
    {
        var userToken = await _userTokenRepository.GetByToken(token);

        if (userToken is null)
        {
            _logger.LogInformation($"User couldn't authenticate with token {token}");
            return AuthenticationServiceResult.Fail(AuthenticationErrorStatus.TokenNotFound);
        }

        if (userToken.Expired)
        {
            _logger.LogInformation($"User with id {userToken.UserId} tried to use expired token {userToken.Token}");
            return AuthenticationServiceResult.Fail(AuthenticationErrorStatus.TokenExpired);
        }

        await _userTokenService.Expire(userToken);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userToken.UserId.ToString())
        };

        var privateKey = _configuration.GetJwtPrivateKey();
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(privateKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var jwtToken = new JwtSecurityToken(
            claims: claims,
            signingCredentials: credentials);

        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        var responseModel = new AuthenticationResponseModel(accessToken);
        
        _logger.LogInformation($"User with id {userToken.UserId} authenticated");
        
        return AuthenticationServiceResult.Success(responseModel);
    }
}