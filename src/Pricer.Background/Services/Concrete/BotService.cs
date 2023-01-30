using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pricer.Background.Services.Abstract;
using Pricer.Bot.Abstract;
using Pricer.Data.InMemory.Repositories.Abstract;
using Pricer.Data.Persistent.Models.Enums;
using Pricer.Data.Persistent.Repositories.Abstract;
using Pricer.Data.Service.Abstract;
using Pricer.Dialog.Models;
using Pricer.Dialog.Services.Abstract;

namespace Pricer.Background.Services.Concrete;

public class BotService : IBotService
{
    private readonly IEnumerable<IBotProviderService> _botProviderServices;
    private readonly IMenuKeyboardBuilder _menuKeyboardBuilder;
    private readonly IUserRepository _userRepository;
    private readonly IMenuRepository _menuRepository;
    private readonly IResourceService _resourceService;

    public BotService(
        IEnumerable<IBotProviderService> botProviderServices,
        IMenuKeyboardBuilder menuKeyboardBuilder,
        IUserRepository userRepository, 
        IMenuRepository menuRepository, 
        IResourceService resourceService)
    {
        _botProviderServices = botProviderServices;
        _menuKeyboardBuilder = menuKeyboardBuilder;
        _userRepository = userRepository;
        _menuRepository = menuRepository;
        _resourceService = resourceService;
    }

    public async Task SendText(BotKey key, string userId, string text)
    {
        await GetBotProviderService(key).SendText(userId, text);
    }

    public async Task SendTextWithMessageKeyboard(BotKey key, string userId, string text, MessageKeyboard keyboard)
    {
        var bot = GetBotProviderService(key);
        
        await bot.SendTextWithMessageKeyboard(userId, text, keyboard);

        if (key == BotKey.Viber)
        {
            var user = await _userRepository.GetByExternalId(userId);
            var menu = _menuRepository.GetByKey(user.MenuKey);
            var menuKeyboard = _menuKeyboardBuilder.Build(menu.Key);
            var menuText = _resourceService.Get(menu.Title);

            await bot.SendTextWithMenuKeyboard(userId, menuText, menuKeyboard);
        }
    }
    
    private IBotProviderService GetBotProviderService(BotKey key)
    {
        return _botProviderServices.First(x => x.Key == key);
    }
}