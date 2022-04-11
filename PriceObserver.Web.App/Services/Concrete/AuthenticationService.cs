using PriceObserver.Web.Shared.Services.Abstract;

namespace PriceObserver.Web.App.Services.Concrete;

public class AuthenticationHttpService : IAuthenticationService
{
    public long GetUserId(string accessToken)
    {
        return default;
    }
}