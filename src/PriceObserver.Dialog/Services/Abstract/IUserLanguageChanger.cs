using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models;
using System.Threading.Tasks;
using PriceObserver.Dialog.Models.Abstract;

namespace PriceObserver.Dialog.Services.Abstract;

public interface IUserLanguageChanger
{
	Task<IReplyResult> Change(User user, LanguageKey languageKey);
}