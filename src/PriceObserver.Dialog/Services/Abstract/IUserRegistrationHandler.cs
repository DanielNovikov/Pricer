using PriceObserver.Data.Persistent.Models;
using PriceObserver.Dialog.Models;

namespace PriceObserver.Dialog.Services.Abstract;

public interface IUserRegistrationHandler
{
    ReplyResult Handle(User user);
}