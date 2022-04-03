using PriceObserver.Data.Models;
using PriceObserver.Web.Shared.Models;

namespace PriceObserver.Web.Api.Services.Abstract;

public interface IItemVmBuilder
{
    ItemVm Build(Item item);
}