using System.Threading.Tasks;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Model.Converters.Abstract;
using PriceObserver.Model.Telegram.Common;
using PriceObserver.Telegram.Dialog.Common.Abstract;
using Telegram.Bot.Types;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Telegram.Dialog.Common.Concrete
{
    public class ChatAuthorizationService : IChatAuthorizationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly IChatToUserConverter _chatToUserConverter;
        
        public ChatAuthorizationService(
            IUserRepository userRepository,
            IUserService userService,
            IChatToUserConverter chatToUserConverter)
        {
            _userRepository = userRepository;
            _userService = userService;
            _chatToUserConverter = chatToUserConverter;
        }

        public async Task<AuthorizationResult> Authorize(Chat chat)
        {
            var userId = chat.Id;
            var user = await _userRepository.GetById(userId);
            
            if (user != null)
                return AuthorizationResult.LoggedIn(user);
            
            user = _chatToUserConverter.Convert(chat);

            var createdUser = await _userService.Create(user);
            
            return AuthorizationResult.Registered(createdUser);
        }
    }
}