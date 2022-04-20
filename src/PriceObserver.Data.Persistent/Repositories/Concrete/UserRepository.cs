using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Data.Persistent.Repositories.Abstract;

namespace PriceObserver.Data.Persistent.Repositories.Concrete;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    { }

    public async Task<User> GetByExternalId(long externalId)
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