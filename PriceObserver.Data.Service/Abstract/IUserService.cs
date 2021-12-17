using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.Models;

namespace PriceObserver.Data.Service.Abstract
{
    public interface IUserService
    {
        Task RedirectToMenu(User user, Menu menu);

        Task DeactivateUserById(long userId);
        
        Task<User> Create(User user);
    }
}