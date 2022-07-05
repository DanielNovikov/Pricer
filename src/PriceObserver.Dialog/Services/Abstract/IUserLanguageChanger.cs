using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Dialog.Models;
using System.Threading.Tasks;

namespace PriceObserver.Dialog.Services.Abstract;

public interface IUserLanguageChanger
{
	Task<ReplyResult> Change(User user, LanguageKey languageKey);
}