using System.Collections.Generic;
using Pricer.Data.InMemory.Models.Abstract;

namespace Pricer.Data.InMemory.Repositories.Abstract;

public interface IReadOnlyRepository<T> where T: IReadonlyEntity
{
    IList<T> GetAll();
}

public interface IReadOnlyRepository<T, in TKey> : IReadOnlyRepository<T> where T: IReadonlyEntity<TKey>
{
    T GetByKey(TKey key);
}