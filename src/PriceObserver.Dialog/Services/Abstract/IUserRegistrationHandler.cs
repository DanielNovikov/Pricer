using PriceObserver.Dialog.Models;
using System.Threading.Tasks;
using PriceObserver.Dialog.Models.Abstract;

namespace PriceObserver.Dialog.Services.Abstract;

public interface IUserRegistrationHandler
{
    Task<IReplyResult> Handle(UserModel userModel);
}