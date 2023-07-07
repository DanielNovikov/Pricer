using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.IdentityModel.Tokens;
using Pricer.Data.Persistent.Repositories.Abstract;
using Pricer.Services.Abstract;

namespace Pricer.Services.Concrete;

public class AuthenticationService : IAuthenticationService
{
    private const string TokenKey = nameof(TokenKey);

    private readonly ProtectedLocalStorage _localStorage;
    private readonly IConfiguration _configuration;
    private readonly IUserTokenRepository _userTokenRepository;
    private readonly ILogger<AuthenticationService> _logger;

    public AuthenticationService(
        ProtectedLocalStorage localStorage,
        IConfiguration configuration,
        IUserTokenRepository userTokenRepository, 
        ILogger<AuthenticationService> logger)
    {
        _localStorage = localStorage;
        _configuration = configuration;
        _userTokenRepository = userTokenRepository;
        _logger = logger;
    }

    public async Task<bool> Login(Guid token)
    {
        var userToken = await _userTokenRepository.GetByToken(token);

        if (userToken is null)
        {
            _logger.LogInformation("User tried to use not existing token '{0}'", token);
            return false;
        }
        
        if (userToken.Expiration < DateTime.UtcNow)
        {
            _logger.LogInformation(
                "User with id '{0}' tried to use expired token '{1}'",
                userToken.UserId,
                userToken.Token);

            return false;
        }
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userToken.UserId.ToString())
        };
        
        var privateKey = _configuration.GetValue<string>("JwtOptions:PrivateKey");
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(privateKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        
        var jwtSecurityToken = new JwtSecurityToken(claims: claims, signingCredentials: credentials);
        var jwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        
        await _localStorage.SetAsync(TokenKey, jwtToken);

        _logger.LogInformation("User authenticated with id " + userToken.UserId);
        
        return true;
    }

    public async Task<int?> GetUserId()
    {
        var claims = await GetUserClaims();

        if (!claims.Any())
            return default;
        
        var userId = claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
        return int.Parse(userId);
    }

    public async Task<List<Claim>> GetUserClaims()
    {
        try
        {
            var tokenResult = await _localStorage.GetAsync<string>(TokenKey);
            if (!tokenResult.Success)
                return new List<Claim>();

            var accessToken = tokenResult.Value;
            var privateKey = _configuration.GetValue<string>("JwtOptions:PrivateKey");
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(privateKey));

            var tokenParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                IssuerSigningKey = securityKey
            };
            
            var validator = new JwtSecurityTokenHandler();

            if (!validator.CanReadToken(tokenResult.Value))
                throw new InvalidOperationException($"Token \"{accessToken}\" couldn't be read");

            return validator.ValidateToken(accessToken, tokenParameters, out _).Claims.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError("GetUserClaims - an error occurred.\nMessage: {0}\nInner Exception: {1}", ex.Message, ex.InnerException);
            return new List<Claim>();
        }
    }

    public async Task Logout()
    {
        await _localStorage.DeleteAsync(TokenKey);
    }
}