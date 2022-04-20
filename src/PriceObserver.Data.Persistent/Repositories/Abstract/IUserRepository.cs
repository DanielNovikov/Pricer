using System.Collections.Generic;
using System.Threading.Tasks;
using PriceObserver.Data.Persistent.Models;

namespace PriceObserver.Data.Persistent.Repositories.Abstract;

public interface IUserRepository
{
    Task<User> GetByExternalId(long externalId);

    Task<IList<User>> GetAllActive();

    Task Add(User user);

    Task Update(User user);
}