using System.Threading.Tasks;
using Pricer.Common.Services.Abstract;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Repositories.Abstract;

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