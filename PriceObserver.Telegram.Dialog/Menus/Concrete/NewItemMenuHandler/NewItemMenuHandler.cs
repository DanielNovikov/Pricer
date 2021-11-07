using System;
using System.Threading.Tasks;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Model.Data;
using PriceObserver.Model.Data.Enums;
using PriceObserver.Model.Telegram.Menu;
using PriceObserver.Parser.Abstract;
using PriceObserver.Telegram.Dialog.Common.Extensions;
using PriceObserver.Telegram.Dialog.Menus.Abstract;
using Telegram.Bot.Types;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Telegram.Dialog.Menus.Concrete.NewItemMenuHandler
{
    public class NewItemMenuHandler : IMenuInputHandler
    {
        private readonly IParserService _parserService;
        private readonly IItemRepository _itemRepository;
        private readonly IShopRepository _shopRepository;

        public NewItemMenuHandler(
            IParserService parserService,
            IItemRepository itemRepository,
            IShopRepository shopRepository)
        {
            _parserService = parserService;
            _itemRepository = itemRepository;
            _shopRepository = shopRepository;
        }

        public MenuType Type => MenuType.NewItem;
        
        public async Task<MenuInputHandlingServiceResult> Handle(Update update, User user)
        {
            var message = update.GetMessageText();

            if (!Uri.TryCreate(message, UriKind.Absolute, out var url))
                return MenuInputHandlingServiceResult.Fail("Ссылка в неверном формате ❌");
            
            var parseResult = await _parserService.Parse(url);

            if (!parseResult.IsSuccess)
                return MenuInputHandlingServiceResult.Fail(parseResult.Error);

            var parsedItem = parseResult.Result;
            var shop = await _shopRepository.GetByType(parsedItem.ShopType);
            
            var item = new Item
            {
                Price = parsedItem.Price,
                Url = url,
                Title = parsedItem.Title,
                UserId = user.Id, 
                ShopId = shop.Id
            };

            await _itemRepository.Add(item);

            return MenuInputHandlingServiceResult.Success("Успешно добавлено! ✅");
        }
    }
}