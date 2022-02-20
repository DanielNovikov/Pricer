using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Services.Abstract;
using PriceObserver.Dialog.Services.Models;

namespace PriceObserver.Dialog.Services.Concrete;

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

    public ReplyResult Build(Menu menu)
    {
        var keyboard = _menuKeyboardBuilder.Build(menu.Key);
        var menuText = _resourceService.Get(menu.ResourceKey);
            
        return ReplyResult.ReplyWithKeyboard(menuText, keyboard);
    }
}