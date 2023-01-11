using System.Security.Claims;
using Pricer.Web.Api.Services.Abstract;
using Pricer.Web.Shared.Services.Abstract;

namespace Pricer.Web.Api.Services.Concrete;

public class UserAuthenticationService : IUserAuthenticationService
{
    private readonly IJwtService _jwtService;

    public UserAuthenticationService(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }

    public int GetUserId(string accessToken)
    {
        var claims = _jwtService.Parse(accessToken);
        var userIdString = claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
        
        return int.Parse(userIdString);
    }
}