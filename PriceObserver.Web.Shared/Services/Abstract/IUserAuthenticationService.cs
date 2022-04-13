namespace PriceObserver.Web.Shared.Services.Abstract;

public interface IUserAuthenticationService
{
    long GetUserId(string accessToken);
}