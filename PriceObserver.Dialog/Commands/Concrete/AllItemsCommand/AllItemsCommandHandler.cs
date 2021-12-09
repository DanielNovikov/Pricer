using System;
using System.Linq;
using System.Threading.Tasks;
using PriceObserver.Data.Models;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
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
        private readonly IResourceService _resourceService;
        
        public AllItemsCommandHandler(
            IItemRepository itemRepository,
            IUserActionLogger userActionLogger, 
            IResourceService resourceService)
        {
            _itemRepository = itemRepository;
            _userActionLogger = userActionLogger;
            _resourceService = resourceService;
        }

        public CommandType Type => CommandType.AllItems; 
        
        public async Task<CommandHandlingServiceResult> Handle(User user)
        {
            _userActionLogger.LogAllItemsCalled(user);
            
            var items = await _itemRepository.GetByUserId(user.Id);

            if (!items.Any())
                return CommandHandlingServiceResult.Fail(ResourceKey.Dialog_EmptyCart);
            
            var message = items
                .Select(x =>
                {
                    var itemPriceMessage = _resourceService.Get(ResourceKey.Dialog_ItemInfo, x.Url, x.Price);
                    return $"{x.Title}{Environment.NewLine}{itemPriceMessage}";
                })
                .Aggregate((x, y) => $"{x}{Environment.NewLine}{Environment.NewLine}{y}");

            var replyResult = ReplyResult.Reply(message);

            return CommandHandlingServiceResult.Success(replyResult);
        }
    }
}