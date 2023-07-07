using System.Security.Claims;

namespace Pricer.Services.Abstract;

public interface IAuthenticationService
{
    Task<bool> Login(Guid token);

    Task<int?> GetUserId();

    Task<List<Claim>> GetUserClaims();

    Task Logout();
}