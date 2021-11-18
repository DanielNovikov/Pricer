using System.Threading.Tasks;
using PriceObserver.Data.Models;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Commands.Abstract;
using PriceObserver.Dialog.Commands.Models;
using PriceObserver.Dialog.Common.Abstract;
using PriceObserver.Dialog.Common.Models;

namespace PriceObserver.Dialog.Commands.Concrete.WebsiteCommand
{
    public class WebsiteCommandHandler : ICommandHandler
    {
        private readonly IUserTokenRepository _userTokenRepository;
        private readonly IUserTokenService _userTokenService;
        private readonly IUserActionLogger _userActionLogger;
        
        public WebsiteCommandHandler(
            IUserTokenRepository userTokenRepository,
            IUserTokenService userTokenService, 
            IUserActionLogger userActionLogger)
        {
            _userTokenRepository = userTokenRepository;
            _userTokenService = userTokenService;
            _userActionLogger = userActionLogger;
        }

        public CommandType Type => CommandType.Website;
        
        public async Task<CommandHandlingServiceResult> Handle(User user)
        {
            _userActionLogger.LogWebsiteCalled(user);
            
            var userToken = await _userTokenRepository.GetNotExpiredByUserId(user.Id);

            if (userToken is null)
                userToken = await _userTokenService.CreateForUser(user.Id);

            var url = $"https://priceobserver.com/login/{userToken.Token}"; // TODO: place my domain
            var message = $"Нажмите на <a href='{url}'>ссылку</a> для перехода на сайт";
            
            var result = ReplyResult.Reply(message);
            return CommandHandlingServiceResult.Success(result);
        }
    }
}