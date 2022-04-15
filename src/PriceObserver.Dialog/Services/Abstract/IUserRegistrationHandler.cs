using PriceObserver.Data.Models;
using PriceObserver.Dialog.Services.Models;

namespace PriceObserver.Dialog.Services.Abstract;

public interface IUserRegistrationHandler
{
    ReplyResult Handle(User user);
}