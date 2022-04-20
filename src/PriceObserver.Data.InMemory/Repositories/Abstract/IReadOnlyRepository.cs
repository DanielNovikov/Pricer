using System.Collections.Generic;
using PriceObserver.Data.InMemory.Models.Abstract;

namespace PriceObserver.Data.InMemory.Repositories.Abstract;

public interface IReadOnlyRepository<T, in TKey> where T: IReadonlyEntity<TKey>
{
    T GetByKey(TKey key);

    IList<T> GetAll();
}