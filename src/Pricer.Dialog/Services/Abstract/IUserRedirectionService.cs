using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.Persistent.Models;
using Pricer.Dialog.Models.Abstract;

namespace Pricer.Dialog.Services.Abstract;

public interface IUserRedirectionService
{
    Task<IReplyResult> Redirect(User user, Menu menuToRedirect);
}