using System.Collections.Generic;
using System.Threading.Tasks;
using PriceObserver.Web.Api.Models;

namespace PriceObserver.Web.Api.Services.Abstract;

public interface IApiItemService
{
    Task<IList<ItemsVm>> GetByUserId(long userId);
        
    Task Delete(int id, long userId);
}