using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Repositories.Abstract;
using Pricer.Common.Services.Abstract;

namespace Pricer.Common.Services.Concrete;

public class UserLanguage : IUserLanguage
{
    private readonly IUserRepository _userRepository;

    public UserLanguage(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task SetForUser(int userId)
    {
        var user = await _userRepository.GetById(userId);
        LanguageKey = user.SelectedLanguageKey;
    }

    public void Set(LanguageKey languageKey)
    {
        LanguageKey = languageKey;
    }

    public LanguageKey? LanguageKey { get; private set; }
}