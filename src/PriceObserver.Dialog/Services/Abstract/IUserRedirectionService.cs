using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Dialog.Models.Abstract;

namespace PriceObserver.Dialog.Services.Abstract;

public interface IUserRedirectionService
{
    Task<IReplyResult> Redirect(User user, Menu menuToRedirect);
}