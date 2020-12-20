using System;
using System.Linq;
using System.Threading.Tasks;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Model.Converters.Abstract;
using PriceObserver.Telegram.Abstract.Client;
using PriceObserver.Telegram.Abstract.Commands.All;
using PriceObserver.Telegram.Concrete.Client;
using Telegram.Bot.Types;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Telegram.Concrete.Commands.All
{
    public class AllCommandService : IAllCommandService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IItemToAllCommandItemDtoConverter _itemToAllCommandItemDtoConverter;
        private readonly ITelegramBotService _telegramBotService; 
        
        public AllCommandService(
            IItemRepository itemRepository, 
            IItemToAllCommandItemDtoConverter itemToAllCommandItemDtoConverter,
            ITelegramBotService telegramBotService)
        {
            _itemRepository = itemRepository;
            _itemToAllCommandItemDtoConverter = itemToAllCommandItemDtoConverter;
            _telegramBotService = telegramBotService;
        }

        public async Task Process(Update update, User user)
        {
            var items = await _itemRepository.GetByUserId(user.Id);
            
            if (!items.Any())
                throw new Exception("You don't have any things yet");

            var itemDtos = items.Select(_itemToAllCommandItemDtoConverter.Convert);

            foreach (var itemDto in itemDtos)
            {
                var message = string.Join(
                    "\r\n",
                    itemDto.Identifier,
                    itemDto.Price,
                    itemDto.Url);

                await _telegramBotService.SendMessage(user.Id, message);
            }
        }
    }
}