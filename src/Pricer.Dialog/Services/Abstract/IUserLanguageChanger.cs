using System.Threading.Tasks;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Models;
using Pricer.Dialog.Models.Abstract;

namespace Pricer.Dialog.Services.Abstract;

public interface IUserLanguageChanger
{
	Task<IReplyResult> Change(User user, LanguageKey languageKey);
}