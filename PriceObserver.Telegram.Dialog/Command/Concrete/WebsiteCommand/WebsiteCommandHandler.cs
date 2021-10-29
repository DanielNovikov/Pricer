using System.Threading.Tasks;
using PriceObserver.Model.Data.Enums;
using PriceObserver.Model.Telegram.Commands;
using PriceObserver.Model.Telegram.Common;
using PriceObserver.Telegram.Dialog.Command.Abstract;
using Telegram.Bot.Types;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Telegram.Dialog.Command.Concrete.WebsiteCommand
{
    public class WebsiteCommandHandler : ICommandHandler
    {
        public CommandType Type => CommandType.Website;
        
        public Task<CommandHandlingServiceResult> Handle(Update update, User user)
        {
            var url = "Нажмите на <a href='https://priceobserver.com'>ссылку</a> для перехода на сайт";
            
            var result = ReplyResult.Reply(url);
            var serviceResult = CommandHandlingServiceResult.Success(result);

            return Task.FromResult(serviceResult);
        }
    }
}