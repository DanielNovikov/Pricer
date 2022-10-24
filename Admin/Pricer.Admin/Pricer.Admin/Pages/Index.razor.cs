using Microsoft.AspNetCore.Components;
using Pricer.Data.Persistent.Models;
using Pricer.Data.Persistent.Repositories.Abstract;
using Pricer.Service.Models;
using Pricer.Service.Services.Abstract;
using Pricer.Telegram.Abstract;

namespace Pricer.Admin.Pages;

public partial class Index : ComponentBase
{
    private string _search = string.Empty;
    
    private UserViewModel[]? _users;
    private UserViewModel[]? _filteredUsers;

    private UserViewModel? _selectedUser;
    private IList<Item>? _items;
    private string _messageToUser = string.Empty;
    
    [Inject]
    public IUserService UserService { get; set; } = default!;

    [Inject]
    public ITelegramBotService TelegramBotService { get; set; } = default!;

    [Inject]
    public IItemRepository ItemRepository { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _users = await UserService.GetAll();
        SearchTextChanged();
    }

    private void SearchTextChanged()
    {
        _filteredUsers = _users?
            .Where(x =>
                x.FullName.Contains(_search, StringComparison.OrdinalIgnoreCase) ||
                x.ExternalId.ToString().Contains(_search, StringComparison.OrdinalIgnoreCase) ||
                (!string.IsNullOrEmpty(x.UserName) && x.UserName.Contains(_search, StringComparison.OrdinalIgnoreCase)))
            .ToArray();
    }

    private async Task OnSelectedUserChanged(UserViewModel user)
    {
        _selectedUser = user;
        _items = await ItemRepository.GetByUserId(user.Id);
    }

    private async Task SendMessageToUser()
    {
        if (_selectedUser is null)
            return;
        
        await TelegramBotService.SendMessage(_selectedUser.ExternalId, _messageToUser);
        _messageToUser = string.Empty;
    }
}