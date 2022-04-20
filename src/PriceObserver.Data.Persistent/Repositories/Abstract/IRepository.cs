using System.Collections.Generic;
using System.Threading.Tasks;
using PriceObserver.Data.Persistent.Models.Abstract;

namespace PriceObserver.Data.Persistent.Repositories.Abstract;

public interface IRepository<T> where T : class, IAggregateRoot
{
    Task<T> GetById(int id);

    Task<IList<T>> GetAll();

    Task Add(T entity);

    Task Update(T entity);

    Task Delete(T entity);
}