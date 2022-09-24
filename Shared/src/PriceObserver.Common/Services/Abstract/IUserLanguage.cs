using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Common.Services.Abstract;

public interface IUserLanguage
{
    Task SetForUser(int userId);
    
    void Set(LanguageKey languageKey);
    
    LanguageKey? LanguageKey { get; }
}