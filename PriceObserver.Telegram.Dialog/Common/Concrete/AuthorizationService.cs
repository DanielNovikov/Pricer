using System.Threading.Tasks;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Model.Converters.Abstract;
using PriceObserver.Model.Telegram.Common;
using PriceObserver.Model.Telegram.Input;
using PriceObserver.Telegram.Dialog.Common.Abstract;

namespace PriceObserver.Telegram.Dialog.Common.Concrete
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly IUpdateDtoToUserConverter _updateDtoConverter;
        
        public AuthorizationService(
            IUserRepository userRepository,
            IUserService userService,
            IUpdateDtoToUserConverter updateDtoConverter)
        {
            _userRepository = userRepository;
            _userService = userService;
            _updateDtoConverter = updateDtoConverter;
        }

        public async Task<AuthorizationResult> Authorize(UpdateDto update)
        {
            var userId = update.UserId;
            var user = await _userRepository.GetById(userId);
            
            if (user != null)
                return AuthorizationResult.LoggedIn(user);
            
            user = _updateDtoConverter.Convert(update);

            var createdUser = await _userService.Create(user);
            
            return AuthorizationResult.Registered(createdUser);
        }
    }
}