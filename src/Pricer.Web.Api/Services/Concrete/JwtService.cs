using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Pricer.Web.Api.Extensions;
using Pricer.Web.Api.Services.Abstract;

namespace Pricer.Web.Api.Services.Concrete;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Create(IEnumerable<Claim> claims)
    {
        var privateKey = _configuration.GetJwtPrivateKey();
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(privateKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var jwtToken = new JwtSecurityToken(
            claims: claims,
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }

    public IEnumerable<Claim> Parse(string accessToken)
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
        return principal.Claims;
    }
}