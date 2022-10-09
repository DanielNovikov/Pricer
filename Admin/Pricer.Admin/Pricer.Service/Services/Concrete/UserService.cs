using Pricer.Data.Persistent.Models;
using Pricer.Data.Persistent.Repositories.Abstract;
using Pricer.Service.Models;
using Pricer.Service.Services.Abstract;

namespace Pricer.Service.Services.Concrete;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserViewModel[]> GetAll()
    {
        var users = await _userRepository.GetAll();

        return users
            .Select(x =>
            {
                var fullName = GetFullName(x);
                
                return new UserViewModel(x.Id, fullName, x.Username, x.ExternalId.ToString(), x.IsActive);
            })
            .ToArray();
    }

    private string GetFullName(User user)
    {
        if (string.IsNullOrEmpty(user.FirstName))
            return user.LastName;

        if (string.IsNullOrEmpty(user.LastName))
            return user.FirstName;

        return $"{user.FirstName} {user.LastName}";
    }
}