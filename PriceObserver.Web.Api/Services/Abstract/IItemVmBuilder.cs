using PriceObserver.Data.Models;
using PriceObserver.Web.Api.Models;

namespace PriceObserver.Web.Api.Services.Abstract;

public interface IItemVmBuilder
{
    ItemVm Build(Item item);
}