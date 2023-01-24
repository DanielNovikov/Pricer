using System.Threading.Tasks;
using Pricer.Common.Services.Abstract;
using Pricer.Data.Persistent.Models;
using Pricer.Data.Persistent.Repositories.Abstract;
using Pricer.Data.Service.Abstract;
using Pricer.Dialog.Models;
using Pricer.Dialog.Services.Abstract;

namespace Pricer.Dialog.Services.Concrete;

public class AuthorizationService : IAuthorizationService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;
    private readonly IUserLanguage _userLanguage;
        
    public AuthorizationService(
        IUserRepository userRepository,
        IUserService userService, 
        IUserLanguage userLanguage)
    {
        _userRepository = userRepository;
        _userService = userService;
        _userLanguage = userLanguage;
    }

    public async Task<User?> LogIn(string externalId)
    {
        var user = await _userRepository.GetByExternalId(externalId);

        if (user is not null)
            _userLanguage.Set(user.SelectedLanguageKey);

        return user;
    }

    public async Task<User> Register(UserModel userModel)
    {
        var user = userModel.ToUser();
        var createdUser = await _userService.Create(user);
        
        _userLanguage.Set(createdUser.SelectedLanguageKey);

        return createdUser;
    }
}