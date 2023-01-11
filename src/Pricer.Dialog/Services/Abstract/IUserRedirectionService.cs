using System.Threading.Tasks;
using Pricer.Data.InMemory.Models;
using Pricer.Data.Persistent.Models;
using Pricer.Dialog.Models.Abstract;

namespace Pricer.Dialog.Services.Abstract;

public interface IUserRedirectionService
{
    Task<IReplyResult> Redirect(User user, Menu menuToRedirect);
}