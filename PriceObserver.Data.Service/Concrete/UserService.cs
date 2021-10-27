using System.Threading.Tasks;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Model.Data;

namespace PriceObserver.Data.Service.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task RedirectToMenu(User user, Menu menu)
        {
            user.Menu = menu;
            return _userRepository.Update(user);
        }
    }
}