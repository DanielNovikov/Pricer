using System.Threading.Tasks;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Dialog.Menus.Abstract;
using PriceObserver.Model.Data;
using PriceObserver.Model.Data.Enums;
using PriceObserver.Model.Telegram.Input;
using PriceObserver.Model.Telegram.Menu;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Dialog.Menus.Concrete.NewItemMenuHandler
{
    public class NewItemMenuHandler : IMenuInputHandler
    {
        private readonly IParserService _parserService;
        private readonly IItemRepository _itemRepository;
        private readonly IShopRepository _shopRepository;
        private readonly IUrlExtractor _urlExtractor;

        public NewItemMenuHandler(
            IParserService parserService,
            IItemRepository itemRepository,
            IShopRepository shopRepository, 
            IUrlExtractor urlExtractor)
        {
            _parserService = parserService;
            _itemRepository = itemRepository;
            _shopRepository = shopRepository;
            _urlExtractor = urlExtractor;
        }

        public MenuType Type => MenuType.NewItem;
        
        public async Task<MenuInputHandlingServiceResult> Handle(MessageDto message)
        {
            var urlExtractionResult = _urlExtractor.Extract(message.Text);

            if (!urlExtractionResult.IsSuccess)
                return MenuInputHandlingServiceResult.Fail(urlExtractionResult.Error);

            var url = urlExtractionResult.Result;

            var itemExists = await _itemRepository.ExistsByUrl(url);
            if (itemExists)
                return MenuInputHandlingServiceResult.Fail("Такой товар уже есть в Вашем списке ☑");
            
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
                ImageUrl = parsedItem.ImageUrl,
                UserId = message.User.Id, 
                ShopId = shop.Id
            };

            await _itemRepository.Add(item);

            return MenuInputHandlingServiceResult.Success("Успешно добавлено! ✅");
        }
    }
}