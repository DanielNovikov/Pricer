using System.Threading.Tasks;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Model.Data;
using PriceObserver.Model.Telegram.Common;
using PriceObserver.Telegram.Dialog.Common.Abstract;
using PriceObserver.Telegram.Dialog.Menus.Abstract;
using CommandType = PriceObserver.Model.Data.Enums.CommandType;

namespace PriceObserver.Telegram.Dialog.Common.Concrete
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
            var addCommandTitle = await _commandRepository.GetTitleByType(CommandType.Add);
            var websiteCommandTitle = await _commandRepository.GetTitleByType(CommandType.Website);

            var shopsInfoMessage = await _shopsInfoMessageBuilder.Build();
            
            var message = $@"Приветствую, {user.FirstName} {user.LastName}! 🎉

Здесь Вы сможете добавить желаемые товары за которыми Вы хотели бы следить и мы оповестим Вас как только цена понизится. ({addCommandTitle})

Так же можно перейти на сайт, где доступно больше возможностей для редактирования Вашего списка товаров в более удобном формате. ({websiteCommandTitle})

{shopsInfoMessage}

{user.Menu.Text}";

            var menuKeyboard = await _menuKeyboardBuilder.Build(user.Menu);

            return ReplyResult.ReplyWithKeyboard(message, menuKeyboard);
        }
    }
}