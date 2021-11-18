using System.Threading.Tasks;
using PriceObserver.Common.Extensions;
using PriceObserver.Data.Models;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Dialog.Common.Abstract;
using PriceObserver.Dialog.Common.Models;
using PriceObserver.Dialog.Input.Abstract;
using PriceObserver.Dialog.Menus.Abstract;

namespace PriceObserver.Dialog.Input.Concrete
{
    public class UserRegistrationHandler : IUserRegistrationHandler
    {
        private readonly ICommandRepository _commandRepository;
        private readonly IMenuKeyboardBuilder _menuKeyboardBuilder;
        private readonly IShopsInfoMessageBuilder _shopsInfoMessageBuilder;
        private readonly IUserActionLogger _userActionLogger;
        
        public UserRegistrationHandler(
            ICommandRepository commandRepository,
            IMenuKeyboardBuilder menuKeyboardBuilder, 
            IShopsInfoMessageBuilder shopsInfoMessageBuilder, 
            IUserActionLogger userActionLogger)
        {
            _commandRepository = commandRepository;
            _menuKeyboardBuilder = menuKeyboardBuilder;
            _shopsInfoMessageBuilder = shopsInfoMessageBuilder;
            _userActionLogger = userActionLogger;
        }

        public async Task<ReplyResult> Handle(User user)
        {
            _userActionLogger.LogUserRegistered(user);
            
            var addCommandTitle = await GetCommandTitle(CommandType.Add);
            var websiteCommandTitle = await GetCommandTitle(CommandType.Website);
            var writeToSupportTitle = await GetCommandTitle(CommandType.WriteToSupport);
            
            var shopsInfoMessage = await _shopsInfoMessageBuilder.Build();
            
            var message = $@"Приветствую, {user.GetFullName()}! 🎉

Здесь Вы сможете добавить желаемые товары за которыми Вы хотели бы следить. Мы оповестим Вас как только цена снизиться. ({addCommandTitle})

Так же можно перейти на сайт, где доступно больше возможностей для редактирования Вашего списка товаров в более удобном формате. ({websiteCommandTitle})

Если у вас есть какое-то пожелание или негодование, можете обратиться в поддержку. ({writeToSupportTitle})

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