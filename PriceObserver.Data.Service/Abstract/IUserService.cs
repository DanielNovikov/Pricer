using System.Threading.Tasks;
using PriceObserver.Model.Data;

namespace PriceObserver.Data.Service.Abstract
{
    public interface IUserService
    {
        Task RedirectToMenu(User user, Menu menu);
    }
}