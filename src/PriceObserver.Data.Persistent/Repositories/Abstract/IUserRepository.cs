using System.Collections.Generic;
using System.Threading.Tasks;
using PriceObserver.Data.Persistent.Models;

namespace PriceObserver.Data.Persistent.Repositories.Abstract;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetByExternalId(long externalId);

    Task<IList<User>> GetAllActive();
}