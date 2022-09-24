using Pricer.Data.Persistent.Models;
using Pricer.Data.Persistent.Repositories.Abstract;

namespace Pricer.Data.Persistent.Repositories.Concrete;

public class ItemPriceChangeRepository : RepositoryBase<ItemPriceChange>, IItemPriceChangeRepository
{
    public ItemPriceChangeRepository(ApplicationDbContext context) : base(context)
    { }
}