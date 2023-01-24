using System.Threading.Tasks;
using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.InMemory.Repositories.Abstract;
using Pricer.Data.Persistent.Models;
using Pricer.Data.Persistent.Repositories.Abstract;
using Pricer.Data.Service.Abstract;

namespace Pricer.Data.Service.Concrete;

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

    public async Task DeactivateUserById(string externalId)
    {
        var user = await _userRepository.GetByExternalId(externalId);
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

    public async Task ChangeMinimumDiscountThreshold(User user, int threshold)
    {
        user.MinimumDiscountThreshold = threshold;
        await _userRepository.Update(user);
    }

    public async Task<User> Create(User user)
    {
        var defaultMenu = _menuRepository.GetDefault();
        user.MenuKey = defaultMenu.Key;
        user.MinimumDiscountThreshold = 5;

        await _userRepository.Add(user);
            
        return await _userRepository.GetByExternalId(user.ExternalId);
    }
}