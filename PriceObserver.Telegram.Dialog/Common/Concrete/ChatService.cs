using System.Threading.Tasks;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Model.Converters.Abstract;
using PriceObserver.Telegram.Dialog.Common.Abstract;
using Telegram.Bot.Types;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Telegram.Dialog.Common.Concrete
{
    public class ChatService : IChatService
    {
        private readonly IUserRepository _userRepository;
        private readonly IChatToUserConverter _chatToUserConverter;
        private readonly IMenuRepository _menuRepository;
        
        public ChatService(
            IUserRepository userRepository,
            IChatToUserConverter chatToUserConverter, 
            IMenuRepository menuRepository)
        {
            _userRepository = userRepository;
            _chatToUserConverter = chatToUserConverter;
            _menuRepository = menuRepository;
        }

        public async Task<User> GetUser(Chat chat)
        {
            var userId = chat.Id;
            var user = await _userRepository.GetById(userId);
            
            if (user == null)
            {
                user = _chatToUserConverter.Convert(chat);

                var defaultMenu = await _menuRepository.GetDefault();
                user.MenuId = defaultMenu.Id;
                
                await _userRepository.Add(user);
            }

            return user;
        }
    }
}