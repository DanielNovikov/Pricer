using Pricer.Service.Models;

namespace Pricer.Service.Services.Abstract;

public interface IItemService
{
    Task<ItemViewModel[]> GetByUserId(int userId);

    Task DeleteItem(int id);
}