using System.Linq;
using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Services.Abstract;
using PriceObserver.Dialog.Services.Models;

namespace PriceObserver.Dialog.Services.Concrete;

public class MenuKeyboardBuilder : IMenuKeyboardBuilder
{
    private readonly ICommandRepository _commandRepository;
    private readonly IMenuRepository _menuRepository;
    private readonly IResourceService _resourceService;
        
    private const int ButtonsInRow = 2; 
        
    public MenuKeyboardBuilder(
        ICommandRepository commandRepository, 
        IMenuRepository menuRepository, 
        IResourceService resourceService)
    {
        _commandRepository = commandRepository;
        _menuRepository = menuRepository;
        _resourceService = resourceService;
    }

    public MenuKeyboard Build(MenuKey menuKey)
    {
        var menu = _menuRepository.GetByKey(menuKey);
        var commands = menu.Commands.ToList();

        if (menu.Parent is not null)
        {
            var backCommand = _commandRepository.GetByKey(CommandKey.Back);
            commands = commands.Append(backCommand).ToList();
        }
            
        var buttonTitles = commands.Select((x, i) => new { Index = i, Text = _resourceService.Get(x.ResourceKey) });

        var buttonRows = buttonTitles
            .GroupBy(x => x.Index / ButtonsInRow)
            .Select(x => x.Select(y => y.Text));

        var buttonsGrid = buttonRows
            .Select(
                x => x
                    .Select(y => new MenuKeyboardButton(y))
                    .ToList())
            .ToList();

        return new MenuKeyboard(buttonsGrid);
    }
}