using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Data.Persistent.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;

namespace PriceObserver.Data.Service.Concrete;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMenuRepository _menuRepository;

    public UserService(
        IUserRepository userRepository,
        IMenuRepository menuRepository)
    {
        _userRepository = userRepository;
        _menuRepository = menuRepository;
    }

    public async Task RedirectToMenu(User user, Menu menu)
    {
        user.MenuKey = menu.Key;
            
        await _userRepository.Update(user);
    }

    public async Task DeactivateUserById(long userId)
    {
        var user = await _userRepository.GetByExternalId(userId);
        user.IsActive = false;
            
        await _userRepository.Update(user);
    }

    public async Task ChangeSelectedLanguage(User user, LanguageKey languageKey)
    {
        user.SelectedLanguageKey = languageKey;
        await _userRepository.Update(user);
    }

    public async Task ChangePriceGrowthNotificationsEnabled(User user, bool enabled)
    {
        user.GrowthPriceNotificationsEnabled = enabled;
        await _userRepository.Update(user);
    }

    public async Task<User> Create(User user)
    {
        var defaultMenu = _menuRepository.GetDefault();
        user.MenuKey = defaultMenu.Key;

        await _userRepository.Add(user);
            
        return await _userRepository.GetByExternalId(user.ExternalId);
    }
}