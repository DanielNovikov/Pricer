using System.Collections.Generic;
using PriceObserver.Data.InMemory.Models.Abstract;

namespace PriceObserver.Data.InMemory.Repositories.Abstract;

public interface IReadOnlyRepository<T> where T: IReadonlyEntity
{
    IList<T> GetAll();
}

public interface IReadOnlyRepository<T, in TKey> : IReadOnlyRepository<T> where T: IReadonlyEntity<TKey>
{
    T GetByKey(TKey key);
}