using System.Threading.Tasks;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Common.Services.Abstract;

public interface IUserLanguage
{
    Task SetForUser(int userId);
    
    void Set(LanguageKey languageKey);
    
    LanguageKey? LanguageKey { get; }
}