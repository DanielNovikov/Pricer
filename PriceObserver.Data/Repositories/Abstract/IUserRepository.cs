using System.Collections.Generic;
using System.Threading.Tasks;
using PriceObserver.Data.Models;

namespace PriceObserver.Data.Repositories.Abstract;

public interface IUserRepository
{
    Task<User> GetById(long id);

    Task<IList<User>> GetAllActive();

    Task Add(User user);

    Task Update(User user);
}