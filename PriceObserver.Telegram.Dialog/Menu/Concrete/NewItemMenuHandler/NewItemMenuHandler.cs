using System;
using System.Threading.Tasks;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Model.Data;
using PriceObserver.Model.Data.Enums;
using PriceObserver.Model.Telegram.Menu;
using PriceObserver.Parser.Abstract;
using PriceObserver.Telegram.Dialog.Common.Extensions;
using PriceObserver.Telegram.Dialog.Menu.Abstract;
using Telegram.Bot.Types;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Telegram.Dialog.Menu.Concrete.NewItemMenuHandler
{
    public class NewItemMenuHandler : IMenuInputHandler
    {
        private readonly IParserService _parserService;
        private readonly IItemRepository _itemRepository;

        public NewItemMenuHandler(
            IParserService parserService,
            IItemRepository itemRepository)
        {
            _parserService = parserService;
            _itemRepository = itemRepository;
        }

        public MenuType Type => MenuType.NewItem;
        
        public async Task<MenuInputHandlingServiceResult> Handle(Update update, User user)
        {
            var message = update.GetMessageText();

            if (!Uri.TryCreate(message, UriKind.Absolute, out var url))
                return MenuInputHandlingServiceResult.Fail("Ссылка в неверном формате ❌");
            
            var parsedItem = await _parserService.Parse(url);

            if (!parsedItem.IsSuccess)
                return MenuInputHandlingServiceResult.Fail(parsedItem.Error);
            
            var item = new Item
            {
                Price = parsedItem.Result.Price,
                Url = url,
                UserId = user.Id
            };

            await _itemRepository.Add(item);

            return MenuInputHandlingServiceResult.Success("Успешно добавлено! ✅");
        }
    }
}