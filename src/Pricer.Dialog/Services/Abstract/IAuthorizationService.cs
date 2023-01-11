using System.Threading.Tasks;
using PriceObserver.Data.Persistent.Models;
using Pricer.Dialog.Models;

namespace Pricer.Dialog.Services.Abstract;

public interface IAuthorizationService
{
    Task<User?> LogIn(long externalId);
    
    Task<User> Register(UserModel userModel);
}