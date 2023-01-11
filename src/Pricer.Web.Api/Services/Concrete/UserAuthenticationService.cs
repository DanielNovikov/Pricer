using System.Security.Claims;
using PriceObserver.Web.Shared.Services.Abstract;
using Pricer.Web.Api.Services.Abstract;

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