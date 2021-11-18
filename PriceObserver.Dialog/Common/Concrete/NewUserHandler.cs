using System.Threading.Tasks;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Dialog.Common.Abstract;
using PriceObserver.Dialog.Menus.Abstract;
using PriceObserver.Model.Converters.Extensions;
using PriceObserver.Model.Data;
using PriceObserver.Model.Dialog.Common;
using CommandType = PriceObserver.Model.Data.Enums.CommandType;

namespace PriceObserver.Dialog.Common.Concrete
{
    public class NewUserHandler : INewUserHandler
    {
        private readonly ICommandRepository _commandRepository;
        private readonly IMenuKeyboardBuilder _menuKeyboardBuilder;
        private readonly IShopRepository _shopRepository;
        private readonly IShopsInfoMessageBuilder _shopsInfoMessageBuilder;
        
        public NewUserHandler(
            ICommandRepository commandRepository,
            IMenuKeyboardBuilder menuKeyboardBuilder, 
            IShopRepository shopRepository, 
            IShopsInfoMessageBuilder shopsInfoMessageBuilder)
        {
            _commandRepository = commandRepository;
            _menuKeyboardBuilder = menuKeyboardBuilder;
            _shopRepository = shopRepository;
            _shopsInfoMessageBuilder = shopsInfoMessageBuilder;
        }

        public async Task<ReplyResult> Handle(User user)
        {
            var addCommandTitle = await GetCommandTitle(CommandType.Add);
            var websiteCommandTitle = await GetCommandTitle(CommandType.Website);

            var shopsInfoMessage = await _shopsInfoMessageBuilder.Build();
            
            var message = $@"Приветствую, {user.GetFullName()}! 🎉

Здесь Вы сможете добавить желаемые товары за которыми Вы хотели бы следить и мы оповестим Вас как только цена понизится. ({addCommandTitle})

Так же можно перейти на сайт, где доступно больше возможностей для редактирования Вашего списка товаров в более удобном формате. ({websiteCommandTitle})

{shopsInfoMessage}

{user.Menu.Text}";

            var menuKeyboard = await _menuKeyboardBuilder.Build(user.Menu);

            return ReplyResult.ReplyWithKeyboard(message, menuKeyboard);
        }

        private async Task<string> GetCommandTitle(CommandType type)
        {
            return (await _commandRepository.GetByType(type)).Title;
        }
    }
}