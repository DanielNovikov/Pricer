using Pricer.Dialog.Common.Models;
using Pricer.Dialog.Common.Models.Abstract;

namespace Pricer.Dialog.Input.Services.Abstract;

public interface IUserRegistrationHandler
{
    Task<IReplyResult> Handle(UserModel userModel);
}