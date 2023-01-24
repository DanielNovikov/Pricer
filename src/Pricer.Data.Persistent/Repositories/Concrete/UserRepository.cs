using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pricer.Data.Persistent.Models;
using Pricer.Data.Persistent.Repositories.Abstract;

namespace Pricer.Data.Persistent.Repositories.Concrete;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    { }

    public async Task<User> GetByExternalId(string externalId)
    {
        return await Context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ExternalId == externalId);
    }

    public async Task<IList<User>> GetAllActive()
    {
        return await Context.Users
            .AsNoTracking()
            .Where(x => x.IsActive)
            .ToListAsync();
    }
}