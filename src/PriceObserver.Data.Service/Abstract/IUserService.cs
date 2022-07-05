using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models;

namespace PriceObserver.Data.Service.Abstract;

public interface IUserService
{
    Task RedirectToMenu(User user, Menu menu);

    Task DeactivateUserById(long userId);

    Task ChangeSelectedLanguage(User user, LanguageKey languageKey);
        
    Task<User> Create(User user);
}