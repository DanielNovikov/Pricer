using System.Threading.Tasks;
using PriceObserver.Model.Data;
using PriceObserver.Model.Dialog.Common;

namespace PriceObserver.Dialog.Input.Abstract
{
    public interface IUserRegistrationHandler
    {
        Task<ReplyResult> Handle(User user);
    }
}