using System.Collections.Generic;
using System.Threading.Tasks;
using Pricer.Data.Persistent.Models;

namespace Pricer.Data.Persistent.Repositories.Abstract;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetByExternalId(string externalId);

    Task<IList<User>> GetAllActive();
}