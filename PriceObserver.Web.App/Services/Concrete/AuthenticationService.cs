using PriceObserver.Web.Shared.Services.Abstract;

namespace PriceObserver.Web.App.Services.Concrete;

public class UserAuthenticationService : IUserAuthenticationService
{
    public long GetUserId(string accessToken)
    {
        return default;
    }
}