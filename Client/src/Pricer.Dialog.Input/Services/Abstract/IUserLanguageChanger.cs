using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Models;
using Pricer.Dialog.Common.Models.Abstract;

namespace Pricer.Dialog.Input.Services.Abstract;

public interface IUserLanguageChanger
{
	Task<IReplyResult> Change(User user, LanguageKey languageKey);
}