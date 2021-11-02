using System.Threading.Tasks;
using PriceObserver.Model.Data;
using PriceObserver.Model.Telegram.Common;

namespace PriceObserver.Telegram.Dialog.Common.Abstract
{
    public interface INewUserHandler
    {
        Task<ReplyResult> Handle(User user);
    }
}