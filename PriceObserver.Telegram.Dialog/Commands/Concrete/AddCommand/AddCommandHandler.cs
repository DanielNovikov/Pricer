using System;
using System.Threading.Tasks;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Model.Data;
using PriceObserver.Model.Data.Enums;
using PriceObserver.Model.Telegram.Commands;
using PriceObserver.Parser.Abstract;
using PriceObserver.Telegram.Dialog.Commands.Abstract;
using PriceObserver.Telegram.Dialog.Common.Extensions;
using Telegram.Bot.Types;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Telegram.Dialog.Commands.Concrete.AddCommand
{
    public class AddCommandHandler : ICommandHandler
    {
        private readonly IParserService _parserService;
        private readonly IItemRepository _itemRepository;

        public AddCommandHandler(
            IParserService parserService,
            IItemRepository itemRepository)
        {
            _parserService = parserService;
            _itemRepository = itemRepository;
        }

        public CommandType Type => CommandType.Add;

        public async Task<CommandHandlingServiceResult> Handle(Update update, User user)
        {
            var message = update.GetMessageText();

            if (!Uri.TryCreate(message, UriKind.Absolute, out var url))
                return CommandHandlingServiceResult.Fail("Ссылка в неверном формате. Пожалуйста, перепроверьте.");
            
            var parsedItem = await _parserService.Parse(url);

            if (!parsedItem.IsSuccess)
                return CommandHandlingServiceResult.Fail(parsedItem.Error);
            
            var item = new Item
            {
                Price = parsedItem.Result.Price,
                Url = url,
                UserId = user.Id
            };

            await _itemRepository.Add(item);

            return CommandHandlingServiceResult.Success("Успешно добавлено!");
        }
    }
}