using Pricer.Models;

namespace Pricer.Services.Abstract;

public interface IUserItemsService
{
    Task<List<ShopDto>> GetByUserId(int userId);

    Task<List<ItemPriceChangeDto>> GetPriceChanges(int id);
}