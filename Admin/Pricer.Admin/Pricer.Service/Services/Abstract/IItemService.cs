using Pricer.Service.Models;

namespace Pricer.Service.Services.Abstract;

public interface IItemService
{
    Task<GlobalItemViewModel[]> GetAll();
    
    Task<UserItemViewModel[]> GetByUserId(int userId);

    Task DeleteItem(int id);
}