using System.Threading.Tasks;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Commands.Abstract;
using PriceObserver.Model.Data.Enums;
using PriceObserver.Model.Dialog.Commands;
using PriceObserver.Model.Dialog.Common;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Dialog.Commands.Concrete.WebsiteCommand
{
    public class WebsiteCommandHandler : ICommandHandler
    {
        private readonly IUserTokenRepository _userTokenRepository;
        private readonly IUserTokenService _userTokenService;
        
        public WebsiteCommandHandler(
            IUserTokenRepository userTokenRepository,
            IUserTokenService userTokenService)
        {
            _userTokenRepository = userTokenRepository;
            _userTokenService = userTokenService;
        }

        public CommandType Type => CommandType.Website;
        
        public async Task<CommandHandlingServiceResult> Handle(User user)
        {
            var userToken = await _userTokenRepository.GetNotExpiredByUserId(user.Id);

            if (userToken == null)
                userToken = await _userTokenService.CreateForUser(user.Id);

            var url = $"https://priceobserver.com/login/{userToken.Token}"; // TODO: place my domain
            var message = $"Нажмите на <a href='{url}'>ссылку</a> для перехода на сайт";
            
            var result = ReplyResult.Reply(message);
            return CommandHandlingServiceResult.Success(result);
        }
    }
}