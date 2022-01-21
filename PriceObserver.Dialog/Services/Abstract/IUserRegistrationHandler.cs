using System.Threading.Tasks;
using PriceObserver.Data.Models;
using PriceObserver.Dialog.Services.Models;

namespace PriceObserver.Dialog.Services.Abstract;

public interface IUserRegistrationHandler
{
    Task<ReplyResult> Handle(User user);
}