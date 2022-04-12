using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Web.Api.Extensions;
using PriceObserver.Web.Shared.Grpc;
using PriceObserver.Web.Shared.Grpc.HandlerServices;

namespace PriceObserver.Web.Api.Handlers;

public class AuthenticationHandlerService : IAuthenticationHandlerService
{
    private readonly IUserTokenRepository _userTokenRepository;
    private readonly IUserTokenService _userTokenService;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthenticationHandlerService> _logger;

    public AuthenticationHandlerService(
        IUserTokenRepository userTokenRepository,
        IUserTokenService userTokenService,
        IConfiguration configuration,
        ILogger<AuthenticationHandlerService> logger)
    {
        _userTokenRepository = userTokenRepository;
        _userTokenService = userTokenService;
        _configuration = configuration;
        _logger = logger;
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
        
        _logger.LogInformation("User with id {0} authenticated", userToken.UserId);
        
        return new AuthenticationReply
        {
            IsSuccess = true, 
            AccessToken = accessToken
        };
    }
}