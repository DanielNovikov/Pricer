using Pricer.Data.InMemory.Models;
using Pricer.Data.Persistent.Models;
using Pricer.Dialog.Common.Models.Abstract;

namespace Pricer.Dialog.Input.Services.Abstract;

public interface IUserRedirectionService
{
    Task<IReplyResult> Redirect(User user, Menu menuToRedirect);
}