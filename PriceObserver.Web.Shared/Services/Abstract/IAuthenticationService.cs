using PriceObserver.Web.Shared.Models;

namespace PriceObserver.Web.Shared.Services.Abstract;

public interface IAuthenticationService
{
    Task<AuthenticationServiceResult> Authenticate(Guid token);
}