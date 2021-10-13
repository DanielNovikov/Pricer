using System.Threading.Tasks;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Model.Telegram.Commands;
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

        public async Task<CommandExecutionResult> Process(Update update, User user)
        {
            var parametersBuildResult = _removeCommandParametersBuilder.Build(update);

            if (!parametersBuildResult.IsSuccess)
                return CommandExecutionResult.Fail(parametersBuildResult.Error);
            
            var itemId = parametersBuildResult.Result.Id;

            var item = await _itemRepository.GetById(itemId);
            if (item == null)
                return CommandExecutionResult.Fail("Item does not exist");
            
            if (item.UserId != user.Id)
                return CommandExecutionResult.Fail("This item is not yours");

            await _itemRepository.Delete(item);

            return CommandExecutionResult.Success("Item deleted successfully!");
        }
    }
}