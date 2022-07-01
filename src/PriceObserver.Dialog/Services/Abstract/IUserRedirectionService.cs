using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Dialog.Models;

namespace PriceObserver.Dialog.Services.Abstract;

public interface IUserRedirectionService
{
    Task<ReplyResult> Redirect(User user, Menu menuToRedirect);
}