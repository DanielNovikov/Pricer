using System.Threading.Tasks;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Input.Abstract;
using PriceObserver.Model.Dialog.Common;
using PriceObserver.Model.Dialog.Input;

namespace PriceObserver.Dialog.Input.Concrete
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        
        public AuthorizationService(
            IUserRepository userRepository,
            IUserService userService)
        {
            _userRepository = userRepository;
            _userService = userService;
        }

        public async Task<AuthorizationResult> Authorize(UpdateDto update)
        {
            var userId = update.UserId;
            var user = await _userRepository.GetById(userId);
            
            if (user is not null)
                return AuthorizationResult.LoggedIn(user);

            user = update.ToUser();

            var createdUser = await _userService.Create(user);
            
            return AuthorizationResult.Registered(createdUser);
        }
    }
}