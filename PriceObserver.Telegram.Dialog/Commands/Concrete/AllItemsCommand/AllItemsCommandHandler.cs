using System;
using System.Linq;
using System.Threading.Tasks;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Model.Data.Enums;
using PriceObserver.Model.Telegram.Commands;
using PriceObserver.Model.Telegram.Common;
using PriceObserver.Telegram.Dialog.Commands.Abstract;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Telegram.Dialog.Commands.Concrete.AllItemsCommand
{
    public class AllItemsCommandHandler : ICommandHandler
    {
        private readonly IItemRepository _itemRepository;

        public AllItemsCommandHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public CommandType Type => CommandType.AllItems; 
        
        public async Task<CommandHandlingServiceResult> Handle(User user)
        {
            var items = await _itemRepository.GetByUserId(user.Id);

            if (!items.Any())
                return CommandHandlingServiceResult.Fail("В вашем списке ещё нет никаких товаров 🗑");
            
            var message = items
                .Select(x => $"{x.Title}{Environment.NewLine}Цена на <a href='{x.Url}'>товар</a> <b>{x.Price}</b>")
                .Aggregate((x, y) => $"{x}{Environment.NewLine}{Environment.NewLine}{y}");

            var replyResult = ReplyResult.Reply(message);

            return CommandHandlingServiceResult.Success(replyResult);
        }
    }
}