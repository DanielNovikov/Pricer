using System;
using System.Threading.Tasks;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Telegram.Abstract.Commands.Remove;
using Telegram.Bot.Types;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Telegram.Concrete.Commands.Remove
{
    public class RemoveCommandService : IRemoveCommandService
    {
        private readonly IRemoveCommandParametersBuilder _removeCommandParametersBuilder;
        private readonly IItemRepository _itemRepository;
        
        public RemoveCommandService(
            IRemoveCommandParametersBuilder removeCommandParametersBuilder,
            IItemRepository itemRepository)
        {
            _removeCommandParametersBuilder = removeCommandParametersBuilder;
            _itemRepository = itemRepository;
        }

        public async Task Process(Update update, User user)
        {
            var parameters = _removeCommandParametersBuilder.Build(update);
            var itemId = parameters.Id;

            var item = await _itemRepository.GetById(itemId);
            if (item == null)
                throw new Exception("Item does not exist");
            
            if (item.UserId != user.Id)
                throw new Exception("This item is not yours");

            await _itemRepository.Delete(item);
        }
    }
}