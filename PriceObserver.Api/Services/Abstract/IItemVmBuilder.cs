using PriceObserver.Api.Models.Response;
using PriceObserver.Data.Models;

namespace PriceObserver.Api.Services.Abstract;

public interface IItemVmBuilder
{
    ItemVm Build(Item item);
}