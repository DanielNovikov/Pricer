using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PriceObserver.Web.Api.Extensions;
using PriceObserver.Web.Shared.Services.Abstract;

namespace PriceObserver.Web.Api.Services.Concrete;

public class AuthenticationService : IAuthenticationService
{
    private readonly IConfiguration _configuration;

    public AuthenticationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public long GetUserId(string accessToken)
    {
        var privateKey = _configuration.GetJwtPrivateKey();
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(privateKey));
                
        var tokenParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            IssuerSigningKey = securityKey
        };

        var validator = new JwtSecurityTokenHandler();

        if (!validator.CanReadToken(accessToken))
            throw new InvalidOperationException("Token couldn't be read");

        var principal = validator.ValidateToken(accessToken, tokenParameters, out _);

        var userIdString = principal.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;

        return long.Parse(userIdString);
    }
}