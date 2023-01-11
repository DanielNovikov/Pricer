namespace PriceObserver.Web.Shared.Services.Abstract;

public interface IUserAuthenticationService
{
    int GetUserId(string accessToken);
}