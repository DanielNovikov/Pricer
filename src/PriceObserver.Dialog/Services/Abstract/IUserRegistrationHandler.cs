using PriceObserver.Data.Persistent.Models;
using PriceObserver.Dialog.Services.Models;

namespace PriceObserver.Dialog.Services.Abstract;

public interface IUserRegistrationHandler
{
    ReplyResult Handle(User user);
}