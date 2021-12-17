using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Common.Models;
using PriceObserver.Dialog.Menus.Abstract;

namespace PriceObserver.Dialog.Menus.Concrete
{
    public class ReplyWithKeyboardBuilder : IReplyWithKeyboardBuilder
    {
        private readonly IMenuKeyboardBuilder _menuKeyboardBuilder;
        private readonly IResourceService _resourceService;

        public ReplyWithKeyboardBuilder(
            IMenuKeyboardBuilder menuKeyboardBuilder,
            IResourceService resourceService)
        {
            _menuKeyboardBuilder = menuKeyboardBuilder;
            _resourceService = resourceService;
        }

        public async Task<ReplyResult> Build(Menu menu)
        {
            var keyboard = await _menuKeyboardBuilder.Build(menu.Key);
            var menuText = _resourceService.Get(menu.ResourceKey);
            
            return ReplyResult.ReplyWithKeyboard(menuText, keyboard);
        }
    }
}