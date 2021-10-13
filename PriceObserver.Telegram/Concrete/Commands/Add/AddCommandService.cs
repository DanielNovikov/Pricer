using System.Threading.Tasks;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Model.Telegram.Commands;
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

        public async Task<CommandExecutionResult> Process(Update update, User user)
        {
            var parametersBuildResult = _addCommandParametersBuilder.Build(update);

            if (!parametersBuildResult.IsSuccess)
                return CommandExecutionResult.Fail(parametersBuildResult.Error);
            
            var itemUrl = parametersBuildResult.Result.Url;
            
            var parsedItem = await _parserService.Parse(itemUrl);

            if (!parsedItem.IsSuccess)
                return CommandExecutionResult.Fail(parsedItem.Error);
            
            var item = _itemBuilder.Build(parsedItem.Result, itemUrl, user.Id);

            await _itemRepository.Add(item);

            return CommandExecutionResult.Success("Item added successfully");
        }
    }
}