using System.Threading.Tasks;
using PriceObserver.Data.Models;
using PriceObserver.Dialog.Common.Models;

namespace PriceObserver.Dialog.Input.Abstract;

public interface IUserRegistrationHandler
{
    Task<ReplyResult> Handle(User user);
}