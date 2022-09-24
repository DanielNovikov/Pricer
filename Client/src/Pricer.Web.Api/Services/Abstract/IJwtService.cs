using System.Security.Claims;

namespace Pricer.Web.Api.Services.Abstract;

public interface IJwtService
{
    string Create(IEnumerable<Claim> claims);

    IEnumerable<Claim> Parse(string accessToken);
}