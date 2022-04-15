using System.Security.Claims;
using PriceObserver.Web.Api.Services.Abstract;
using PriceObserver.Web.Shared.Services.Abstract;

namespace PriceObserver.Web.Api.Services.Concrete;

public class UserAuthenticationService : IUserAuthenticationService
{
    private readonly IJwtService _jwtService;

    public UserAuthenticationService(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }

    public long GetUserId(string accessToken)
    {
        var claims = _jwtService.Parse(accessToken);
        var userIdString = claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
        
        return long.Parse(userIdString);
    }
}