using PriceObserver.Data.Persistent.Models;
using PriceObserver.Data.Persistent.Repositories.Abstract;

namespace PriceObserver.Data.Persistent.Repositories.Concrete;

public class ItemPriceChangeRepository : RepositoryBase<ItemPriceChange>, IItemPriceChangeRepository
{
    public ItemPriceChangeRepository(ApplicationDbContext context) : base(context)
    { }
}