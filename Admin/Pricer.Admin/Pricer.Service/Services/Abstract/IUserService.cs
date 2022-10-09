using Pricer.Service.Models;

namespace Pricer.Service.Services.Abstract;

public interface IUserService
{
    Task<UserViewModel[]> GetAll();
}