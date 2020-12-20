using System.Threading.Tasks;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Parser.Abstract;
using PriceObserver.Telegram.Abstract.Commands.Add;
using Telegram.Bot.Types;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Telegram.Concrete.Commands.Add
{
    public class AddCommandService : IAddCommandService
    {
        private readonly IAddCommandParametersBuilder _addCommandParametersBuilder;
        private readonly IParserService _parserService;
        private readonly IItemBuilder _itemBuilder;
        private readonly IItemRepository _itemRepository;
        
        public AddCommandService(
            IAddCommandParametersBuilder addCommandParametersBuilder,
            IParserService parserService, 
            IItemBuilder itemBuilder,
            IItemRepository itemRepository)
        {
            _addCommandParametersBuilder = addCommandParametersBuilder;
            _parserService = parserService;
            _itemBuilder = itemBuilder;
            _itemRepository = itemRepository;
        }

        public async Task Process(Update update, User user)
        {
            var parameters = _addCommandParametersBuilder.Build(update);
            var itemUrl = parameters.Url;
            
            var parsedItem = await _parserService.Parse(itemUrl);

            var item = _itemBuilder.Build(parsedItem, itemUrl, user.Id);

            await _itemRepository.Add(item);
        }
    }
}