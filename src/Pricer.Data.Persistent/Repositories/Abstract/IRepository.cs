using System.Collections.Generic;
using System.Threading.Tasks;
using Pricer.Data.Persistent.Models.Abstract;

namespace Pricer.Data.Persistent.Repositories.Abstract;

public interface IRepository<T> where T : class, IAggregateRoot
{
    Task<T> GetById(int id);

    Task<IList<T>> GetAll();

    Task Add(T entity);

    Task Update(T entity);

    Task Delete(T entity);
}