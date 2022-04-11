namespace PriceObserver.Web.Shared.Services.Abstract;

public interface IAuthenticationService
{
    long GetUserId(string accessToken);
}