using System;
using System.Linq;
using System.Threading.Tasks;
using PriceObserver.Data.Models;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Dialog.Commands.Abstract;
using PriceObserver.Dialog.Commands.Models;
using PriceObserver.Dialog.Common.Abstract;
using PriceObserver.Dialog.Common.Models;

namespace PriceObserver.Dialog.Commands.Concrete.AllItemsCommand
{
    public class AllItemsCommandHandler : ICommandHandler
    {
        private readonly IItemRepository _itemRepository;
        private readonly IUserActionLogger _userActionLogger;
        
        public AllItemsCommandHandler(
            IItemRepository itemRepository,
            IUserActionLogger userActionLogger)
        {
            _itemRepository = itemRepository;
            _userActionLogger = userActionLogger;
        }

        public CommandType Type => CommandType.AllItems; 
        
        public async Task<CommandHandlingServiceResult> Handle(User user)
        {
            _userActionLogger.LogAllItemsCalled(user);
            
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