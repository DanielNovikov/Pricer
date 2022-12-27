using Microsoft.AspNetCore.Components;
using Pricer.Service.Models;
using Pricer.Service.Services.Abstract;
using Pricer.Telegram.Abstract;
using Telegram.Bot.Types;

namespace Pricer.Admin.Pages;

public partial class Global
{
    private string? _message;

    private GlobalItemViewModel[]? _items;

    [Inject] public IUserService UserService { get; set; } = default!;

    [Inject] public IItemService ItemService { get; set; } = default!;
    
    [Inject] public ITelegramBotService TelegramBotService { get; set; } = default!;
    
    protected override async Task OnInitializedAsync()
    {
        _items = await ItemService.GetAll();
    }
    
    private async Task SendMessage()
    {
        var users = await UserService.GetAll();

        foreach (var user in users)
        {
            await TelegramBotService.SendMessage(user.ExternalId, _message);
            _message = string.Empty;
        }
    }

    private async Task DeleteItem(int id)
    {
        _items = _items?
            .Where(x => x.Id != id)
            .ToArray();
        
        await ItemService.DeleteItem(id);
    }
}