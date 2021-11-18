using System.Threading.Tasks;
using PriceObserver.Data.Models;

namespace PriceObserver.Data.Service.Abstract
{
    public interface IUserService
    {
        Task RedirectToMenu(User user, Menu menu);

        Task<User> Create(User user);
    }
}