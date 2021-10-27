using System.Threading.Tasks;
using PriceObserver.Model.Data.Enums;
using PriceObserver.Model.Telegram.Commands;
using PriceObserver.Telegram.Dialog.Commands.Abstract;
using Telegram.Bot.Types;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Telegram.Dialog.Commands.Concrete.WebsiteCommand
{
    public class WebsiteCommandHandler : ICommandHandler
    {
        public CommandType Type => CommandType.Website;
        
        public Task<CommandHandlingServiceResult> Handle(Update update, User user)
        {
            var url = "<a href='https://priceobserver.com'>Ссылка</a> для перехода на сайт";
            var serviceResult = CommandHandlingServiceResult.Success(url);

            return Task.FromResult(serviceResult);
        }
    }
}