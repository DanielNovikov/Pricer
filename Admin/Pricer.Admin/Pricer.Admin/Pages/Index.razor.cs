using Microsoft.AspNetCore.Components;
using Pricer.Service.Models;
using Pricer.Service.Services.Abstract;

namespace Pricer.Admin.Pages;

public partial class Index : ComponentBase
{
    private string _search = string.Empty;
    
    private UserViewModel[]? _users;
    private UserViewModel[]? _filteredUsers;

    private UserViewModel? _selectedUser;
    
    [Inject]
    public IUserService UserService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _users = await UserService.GetAll();
        SearchTextChanged();
    }

    private void SearchTextChanged()
    {
        _filteredUsers = _users?
            .Where(x =>
                x.FullName.Contains(_search) ||
                x.ExternalId.Contains(_search) ||
                (!string.IsNullOrEmpty(x.UserName) && x.UserName.Contains(_search)))
            .ToArray();
    }

    private void OnSelectedUserChanged(UserViewModel user)
    {
        _selectedUser = user;
    }
}