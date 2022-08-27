using PriceObserver.Data.Persistent.Models;
using System.Threading.Tasks;
using PriceObserver.Dialog.Models;

namespace PriceObserver.Dialog.Services.Abstract;

public interface IAuthorizationService
{
    Task<User?> LogIn(long externalId);
    
    Task<User> Register(UserModel userModel);
}