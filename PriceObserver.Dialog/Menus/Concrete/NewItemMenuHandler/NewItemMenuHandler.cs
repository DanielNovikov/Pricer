using System.Threading.Tasks;
using PriceObserver.Data.Models;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Dialog.Common.Abstract;
using PriceObserver.Dialog.Input.Models;
using PriceObserver.Dialog.Menus.Abstract;
using PriceObserver.Dialog.Menus.Models;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Dialog.Menus.Concrete.NewItemMenuHandler
{
    public class NewItemMenuHandler : IMenuInputHandler
    {
        private readonly IParserService _parserService;
        private readonly IItemRepository _itemRepository;
        private readonly IShopRepository _shopRepository;
        private readonly IUrlExtractor _urlExtractor;
        private readonly IUserActionLogger _userActionLogger;

        public NewItemMenuHandler(
            IParserService parserService,
            IItemRepository itemRepository,
            IShopRepository shopRepository, 
            IUrlExtractor urlExtractor,
            IUserActionLogger userActionLogger)
        {
            _parserService = parserService;
            _itemRepository = itemRepository;
            _shopRepository = shopRepository;
            _urlExtractor = urlExtractor;
            _userActionLogger = userActionLogger;
        }

        public MenuType Type => MenuType.NewItem;
        
        public async Task<MenuInputHandlingServiceResult> Handle(MessageDto message)
        {
            var urlExtractionResult = _urlExtractor.Extract(message.Text);

            if (!urlExtractionResult.IsSuccess)
            {
                _userActionLogger.LogWrongUrlPassed(message.User, message.Text, urlExtractionResult.Error);
                return MenuInputHandlingServiceResult.Fail(urlExtractionResult.Error);
            }

            var url = urlExtractionResult.Result;

            var itemExists = await _itemRepository.ExistsByUrl(url);
            if (itemExists)
            {
                _userActionLogger.LogDuplicateItem(message.User, url);
                return MenuInputHandlingServiceResult.Fail("Такой товар уже есть в Вашем списке ☑");
            }

            var parseResult = await _parserService.Parse(url);

            if (!parseResult.IsSuccess)
            {
                _userActionLogger.LogParsingError(message.User, url, parseResult.Error);
                return MenuInputHandlingServiceResult.Fail(parseResult.Error);
            }

            var parsedItem = parseResult.Result;
            var shop = await _shopRepository.GetByType(parsedItem.ShopType);
            
            var item = new Item
            {
                Price = parsedItem.Price,
                Url = url,
                Title = parsedItem.Title,
                ImageUrl = parsedItem.ImageUrl,
                UserId = message.User.Id, 
                ShopId = shop.Id
            };

            await _itemRepository.Add(item);

            _userActionLogger.LogItemAdded(message.User, item);
            
            return MenuInputHandlingServiceResult.Success("Успешно добавлено! ✅");
        }
    }
}