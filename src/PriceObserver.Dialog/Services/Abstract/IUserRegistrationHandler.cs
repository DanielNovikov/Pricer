using PriceObserver.Dialog.Models;
using System.Threading.Tasks;

namespace PriceObserver.Dialog.Services.Abstract;

public interface IUserRegistrationHandler
{
    Task<ReplyResult> Handle(UserModel userModel);
}