using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Data.Models;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;

namespace PriceObserver.Data.Service.Concrete;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMenuRepository _menuRepository;
    private readonly ILogger _logger;

    public UserService(
        IUserRepository userRepository,
        IMenuRepository menuRepository,
        ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _menuRepository = menuRepository;
        _logger = logger;
    }

    public async Task RedirectToMenu(User user, Menu menu)
    {
        user.MenuKey = menu.Key;
            
        await _userRepository.Update(user);
    }

    public async Task DeactivateUserById(long userId)
    {
        var user = await _userRepository.GetById(userId);
        user.IsActive = false;
            
        await _userRepository.Update(user);
    }

    public async Task<User> Create(User user)
    {
        var defaultMenu = _menuRepository.GetDefault();
        user.MenuKey = defaultMenu.Key;

        await _userRepository.Add(user);
            
        return await _userRepository.GetById(user.Id);
    }
}