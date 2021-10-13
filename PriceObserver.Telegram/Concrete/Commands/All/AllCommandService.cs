using System.Linq;
using System.Threading.Tasks;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Model.Converters.Abstract;
using PriceObserver.Model.Telegram.Commands;
using PriceObserver.Telegram.Abstract.Client;
using PriceObserver.Telegram.Abstract.Commands.All;
using Telegram.Bot.Types;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Telegram.Concrete.Commands.All
{
    public class AllCommandService : IAllCommandService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IItemToItemDtoConverter _itemToItemDtoConverter;
        private readonly ITelegramBotService _telegramBotService; 
        
        public AllCommandService(
            IItemRepository itemRepository, 
            IItemToItemDtoConverter itemToItemDtoConverter,
            ITelegramBotService telegramBotService)
        {
            _itemRepository = itemRepository;
            _itemToItemDtoConverter = itemToItemDtoConverter;
            _telegramBotService = telegramBotService;
        }

        public async Task<CommandExecutionResult> Process(Update update, User user)
        {
            var items = await _itemRepository.GetByUserId(user.Id);
            
            if (!items.Any())
                return CommandExecutionResult.Fail("You don't have any things yet");

            var itemDtos = items.Select(_itemToItemDtoConverter.Convert);

            foreach (var itemDto in itemDtos)
            {
                var message = string.Join(
                    "\r\n",
                    itemDto.Identifier,
                    itemDto.Price,
                    itemDto.Url);

                await _telegramBotService.SendMessage(user.Id, message);
            }

            return CommandExecutionResult.Success();
        }
    }
}