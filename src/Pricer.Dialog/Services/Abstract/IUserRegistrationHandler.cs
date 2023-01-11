using System.Threading.Tasks;
using Pricer.Dialog.Models;
using Pricer.Dialog.Models.Abstract;

namespace Pricer.Dialog.Services.Abstract;

public interface IUserRegistrationHandler
{
    Task<IReplyResult> Handle(UserModel userModel);
}