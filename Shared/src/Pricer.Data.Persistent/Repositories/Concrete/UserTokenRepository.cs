using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pricer.Data.Persistent.Models;
using Pricer.Data.Persistent.Repositories.Abstract;

namespace Pricer.Data.Persistent.Repositories.Concrete;

public class UserTokenRepository : RepositoryBase<UserToken>, IUserTokenRepository
{
    public UserTokenRepository(ApplicationDbContext context) : base(context)
    { }

    public async Task<UserToken> GetNotExpiredByUserId(long userId)
    {
        return await Context.UserTokens
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Expiration > DateTime.UtcNow && x.UserId == userId);
    }

    public async Task<UserToken> GetByToken(Guid token)
    {
        return await Context.UserTokens
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Token == token);
    }
}