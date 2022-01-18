using PriceObserver.Api.Services.Models.Service;

namespace PriceObserver.Api.Services.Abstract;

public interface IAuthenticationService
{
    Task<AuthenticationServiceResult> Authenticate(Guid token);
}