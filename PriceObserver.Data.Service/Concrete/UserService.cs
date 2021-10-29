using System.Threading.Tasks;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Model.Data;

namespace PriceObserver.Data.Service.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMenuRepository _menuRepository;
        
        public UserService(
            IUserRepository userRepository,
            IMenuRepository menuRepository)
        {
            _userRepository = userRepository;
            _menuRepository = menuRepository;
        }

        public async Task RedirectToMenu(User user, Menu menu)
        {
            await _userRepository.UpdateMenu(user.Id, menu);
        }

        public async Task<User> Create(User user)
        {
            var defaultMenu = await _menuRepository.GetDefault();
            user.MenuId = defaultMenu.Id;

            await _userRepository.Add(user);
            
            return await _userRepository.GetById(user.Id);
        }
    }
}