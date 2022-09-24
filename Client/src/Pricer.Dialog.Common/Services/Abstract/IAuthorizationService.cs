using Pricer.Data.Persistent.Models;
using Pricer.Dialog.Common.Models;

namespace Pricer.Dialog.Common.Services.Abstract;

public interface IAuthorizationService
{
    Task<User?> LogIn(long externalId);
    
    Task<User> Register(UserModel userModel);
}