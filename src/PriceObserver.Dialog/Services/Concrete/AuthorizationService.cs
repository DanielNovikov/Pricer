using System.Threading.Tasks;
using PriceObserver.Data.Persistent.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Services.Abstract;
using PriceObserver.Dialog.Services.Models;

namespace PriceObserver.Dialog.Services.Concrete;

public class AuthorizationService : IAuthorizationService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;
        
    public AuthorizationService(
        IUserRepository userRepository,
        IUserService userService)
    {
        _userRepository = userRepository;
        _userService = userService;
    }

    public async Task<AuthorizationResult> Authorize(UpdateServiceModel update)
    {
        var userId = update.UserId;
        var user = await _userRepository.GetById(userId);
            
        if (user is not null)
            return AuthorizationResult.LoggedIn(user);

        user = update.ToUser();

        var createdUser = await _userService.Create(user);
            
        return AuthorizationResult.Registered(createdUser);
    }
}