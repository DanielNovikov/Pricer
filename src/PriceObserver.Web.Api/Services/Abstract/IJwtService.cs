using System.Security.Claims;

namespace PriceObserver.Web.Api.Services.Abstract;

public interface IJwtService
{
    string Create(IEnumerable<Claim> claims);

    IEnumerable<Claim> Parse(string accessToken);
}