using System.Threading.Tasks;
using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Models;

namespace Pricer.Data.Service.Abstract;

public interface IUserService
{
    Task RedirectToMenu(User user, Menu menu);

    Task DeactivateUserById(long userId);

    Task ChangeSelectedLanguage(User user, LanguageKey languageKey);

    Task ChangePriceGrowthNotificationsEnabled(User user, bool enabled);

    Task ChangeMinimumDiscountThreshold(User user, int threshold);
        
    Task<User> Create(User user);
}